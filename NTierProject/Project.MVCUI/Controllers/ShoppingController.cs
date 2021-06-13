using PagedList;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.DTO.Models;
using Project.ENTITIES.Models;
using Project.MVCUI.Models.ShoppingTools;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class ShoppingController : Controller
    {
        ProductRep _pRep;
        CategoryRep _cRep;
        OrderDetailRep _odRep;
        OrderRep _oRep;
        UserProfileRep _upRep;
        public ShoppingController()
        {
            //Bize kısayol sağlayacak repository'lerimizin instance'ını aldık
            _pRep = new ProductRep();
            _cRep = new CategoryRep();
            _oRep = new OrderRep();
            _odRep = new OrderDetailRep();
            _upRep = new UserProfileRep();
        }

        //page ve categoryID nullable çünkü: sayfa ilk açıldığında page parametresine değer gelmicek ve belli bir kategorinin ürünlerinide görmek istemeyebilir kullanıcı
        public ActionResult ShoppingList(int? page, int? categoryID)
        {
            PAVM pavm = new PAVM
            {
                //categoryID gelmiyorsa aktif kategorileri, geliyorsa seçilen kategoriyi döndür
                //page gelmiyorsa 1 olarak, geliyorsa seçilen page'i döndür
                PagedProducts = categoryID == null ? _pRep.GetActives().ToPagedList(page ?? 1, 12) : _pRep.Where(x => x.CategoryID == categoryID).ToPagedList(page ?? 1, 12),
                Categories = _cRep.GetActives()
            };

            if (categoryID != null) ViewBag.CatID = categoryID;//categoryID varsa tempData'ya ata

            return View(pavm);
        }

        public ActionResult AddToCart(int id)
        {
            Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;//Sepete önceden ürün atıldıysa onu kullan, atılmadıysa yeni bir sepet yarat

            Product cartProduct = _pRep.Find(id);//Seçilen ürünü DB'den bul

            CartItem ci = new CartItem
            {
                //Sepette görünecek CartItem için, DB'deki ürünün göstermek istediğimiz özelliklerini ata
                ID = cartProduct.ID,
                Name = cartProduct.Name,
                Price = cartProduct.UnitPrice,
                ImagePath = cartProduct.ImagePath
            };

            c.SepeteEkle(ci);//Ürünü sepete ekle
            Session["scart"] = c;//Sepetin son halini Session'a ata
            return RedirectToAction("ShoppingList");
        }

        public ActionResult CartPage()
        {
            if (Session["scart"] != null)//Sepette ürün var mı ?
            {
                Cart c = Session["scart"] as Cart;//Varsa unboxing yap

                CartVM cvm = new CartVM
                {
                    Cart = c//VM modelimizdeki sepete, mevcut sepetimizi atadık
                };

                return View(cvm);
            }

            TempData["sepetBos"] = "Sepetinizde ürün bulunmamaktadır";//Sepet boş ise tempData ile bilgi yolladık
            return RedirectToAction("ShoppingList");
        }

        public ActionResult DeleteFromCart(int id)
        {
            if (Session["scart"] != null)//Ürün sepetten silindiğinde, sepette ürün kalmamışsa sepeti sil
            {
                Cart c = Session["scart"] as Cart;
                c.SepettenSil(id);

                if (c.Sepetim.Count == 0)
                {
                    Session.Remove("scart");
                    TempData["sepetBos"] = "Sepetinizde ürün bulunmamaktadır";
                    return RedirectToAction("ShoppingList");
                }
                return RedirectToAction("CartPage");
            }

            return RedirectToAction("ShoppingList");
        }

        public ActionResult ConfirmOrder()
        {
            if (Session["member"] == null)
            {
                TempData["kullanici"] = "Siparişi onaylamak için giriş yapmalısınız";
                return RedirectToAction("Login", "Home");
            }
            else return View();
        }

        [HttpPost]
        public ActionResult ConfirmOrder(PaymentDTO paymentDTO)
        {
            Cart c = Session["scart"] as Cart;

            Order order = new Order();

            paymentDTO.ShoppingPrice = order.TotalPrice = c.TotalPrice; //Sepetin toplam tutarını order ve paymentDTO'ya atadık

            using(HttpClient client=new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44309/api/");//Habeleşilecek API'ın url'i
                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync("Payment/ReceivePayment", paymentDTO);//API'ın belirttiğimiz action'ına modelimizi yolladık

                HttpResponseMessage sonuc;

                try
                {
                    sonuc = postTask.Result; //API'dan kaynaklı sıkıntı varsa projemizi patlatmaması için try'a aldık
                }
                catch (Exception)
                {

                    TempData["hata"] = "Banka bağlantıyı reddetti";
                    return RedirectToAction("ShoppingList");
                }

                

                if (sonuc.IsSuccessStatusCode)//API'daki işlem başarılı ise
                {
                    AppUser user = Session["member"] as AppUser;
                    UserProfile profile = _upRep.Find(user.ID);                  

                    order.AppUserID = user.ID;
                    order.Address = profile.Address;
                    order.UserName = user.UserName; //Order tablomuzda spesifik olarak kullanıcının UserName'ini görmek istedik
                    order.Email = user.Email; //Order tablomuzda spesifik olarak kullanıcının Email adresini görmek istedik

                    _oRep.Add(order); //Çoka-çok ilişkideki table'a data girmek için ID'sini oluşturduk

                    foreach (CartItem item in c.Sepetim)
                    {
                        OrderDetail od = new OrderDetail
                        {
                            OrderID = order.ID,
                            ProductID = item.ID,
                            TotalPrice = item.SubTotal,
                            Quantity = item.Amount
                        };
                        _odRep.Add(od);

                        //Satın alınan ürünler stoktan düşüldü
                        Product stokDus = _pRep.Find(item.ID);
                        stokDus.UnitInStock -= item.Amount;
                        _pRep.Update(stokDus);
                    }

                    TempData["odeme"] = "Siparişiniz alınmıştır, teşekkür ederiz";

                    MailService.Send(user.Email, subject: "Sipariş", body: $"Siparişiniz başarıyla alındı, sipariş tutarınız: {order.TotalPrice}");
                    return RedirectToAction("ShoppingList");
                }
                else
                {
                    TempData["sorun"] = "Ödeme ile ilgili bir sorun oluştu, lütfen bankanızla iletişime geçin";
                    return RedirectToAction("ShoppingList");
                }
            }
        }
    }
}