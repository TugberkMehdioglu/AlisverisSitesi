using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        AppUserRep _auRep; //Kısayollarımı kullanmak için Repository class'ımı kullanıyorum
        public HomeController()
        {
            _auRep = new AppUserRep();//Repository'mizi kullanabilmek için instance'ını aldık
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AppUser appUser)
        {
            AppUser kontrol = _auRep.FirstOrDefault(x => x.UserName == appUser.UserName);//Kullanıcı DB'de mevcut mu?

            if (kontrol == null)
            {
                ViewBag.Kullanici = "Kullanıcı bulunamadı";
                return View();//Kullanıcı DB'de mevcut değilse bildirimle view'a geri yolladık
            }

            string decrypt = DantexCryptex.DeCrypt(kontrol.Password);//Şifreyi decrypted etmeliyizki şifrenin sorgulamasını yapabilelim (DB'de kullanıcının şifresi crypted tutulduğu için)
            if (decrypt == appUser.Password && kontrol.Role == UserRole.Admin)//Kullanıcının rolünü sorguladık
            {
                if (!kontrol.Active) return ControlActive();//Mail onayı yoksa method'a yönlendirdik

                if (Session["member"] != null) Session.Remove("member");

                Session["admin"] = kontrol;
                TempData["merhaba"] = "Hoşgeldin admin";//İsim Soyisim veremiyoruz çünkü admin'e profile  atamadık
                return RedirectToAction("CategoryList", "Category", new { Area="Admin"});
            }
            else if (decrypt == appUser.Password && kontrol.Role == UserRole.Member)
            {
                if (!kontrol.Active) return ControlActive();//Mail onayı yoksa method'a yönlendirdik

                if (Session["admin"] != null) Session.Remove("admin");

                Session["member"] = kontrol;
                TempData["merhaba"] = $"Hoşgeldin {kontrol.Profile.FullName}";//Kişinin isim ve soyismiyle karşılama yaptık
                return RedirectToAction("ShoppingList", "Shopping");
            }

            ViewBag.Kullanici = "Kullanıcı adı veya şifre hatalı";
            return View();
        }

        public ActionResult ControlActive()
        {
            ViewBag.AktifDegil = "Hesabınız aktif değil, lütfen mail'inizi kontrol ediniz";
            return View("Login");
        }

        public ActionResult LogOut()
        {
            Session.Remove("member");
            Session.Remove("admin");
            return RedirectToAction("ShoppingList", "Shopping");
        }
    }
}