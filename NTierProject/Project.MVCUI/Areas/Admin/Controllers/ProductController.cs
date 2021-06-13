using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [AdminAuthentication]//Sadece Admin yetkisi olanlar bu controller'a giriş sağlayabilir
    public class ProductController : Controller
    {
        ProductRep _pRep;//Kısayollarımı kullanmak için Repository class'ımı kullanıyorum
        CategoryRep _cRep;//Kısayollarımı kullanmak için Repository class'ımı kullanıyorum
        public ProductController()
        {
            _pRep = new ProductRep();//Repository'mizi kullanabilmek için instance'ını aldık
            _cRep = new CategoryRep();//Repository'mizi kullanabilmek için instance'ını aldık
        }
        public ActionResult ProductList(int? id)//id'li hali için ayrı bir action açmaktansa ternary if ile tek action'da 2 ayrı request'e de cevap vermiş oldum
        {
            //CategoryList'te bu action'a request olduğunda, o kategorinin ürünlerini göstericek
            ProductVM pvm = new ProductVM
            {
                Products = id == null ? _pRep.GetActives() : _pRep.Where(x => x.CategoryID == id)//Ternary if
                //Bu Action'a id parametresine sadece CategoryID gelicek.
            };
            return View(pvm);
        }

        public ActionResult ProductDetail(int id)//Her üründe bu action'a request olduğunda, o ürünün tüm özelliklerini göstericek
        {
            ProductVM pvm = new ProductVM
            {
                Product = _pRep.Find(id)
            };

            return View(pvm);
        }

        public ActionResult AddProduct()
        {
            ProductVM pvm = new ProductVM
            {
                Categories = _cRep.GetActives()
            };

            return View(pvm);
        }

        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase image)
        {
            //Resim yüklenmemiş ise ImagePath property'sini null, resim yüklenmişse ImagePath'e resmin yolunu atıyoruz
            if (image == null) product.ImagePath = null;

            else product.ImagePath = ImageUploader.UploadImage("/Pictures/", image);//Yüklenen resmi ürünün resmi olarak atadık

            _pRep.Add(product);

            return RedirectToAction("ProductList");
        }

        public ActionResult UpdateProduct(int id)
        {
            ProductVM pvm = new ProductVM
            {
                Product = _pRep.Find(id),
                Categories = _cRep.GetActives()
            };
            return View(pvm);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product product, HttpPostedFileBase image)
        {
            //Resim yüklenmemiş ise ImagePath property'sini null, resim yüklenmişse ImagePath'e resmin yolunu atıyoruz
            if (image == null) product.ImagePath = null;

            else product.ImagePath = ImageUploader.UploadImage("/Pictures/", image);//Yüklenen resmi ürünün resmi olarak atadık

            _pRep.Update(product);

            return RedirectToAction("ProductList");
        }

        public ActionResult DeleteProduct(int id)
        {
            _pRep.Delete(_pRep.Find(id));

            return RedirectToAction("ProductList");
        }
    }
}