using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class RegisterController : Controller
    {
        AppUserRep _auRep;//Kısayollarımı kullanmak için Repository class'ımı kullanıyorum
        UserProfileRep _upRep; //Kısayollarımı kullanmak için Repository class'ımı kullanıyorum
        public RegisterController()
        {
            _auRep = new AppUserRep();//Repository'mizi kullanabilmek için instance'ını aldık
            _upRep = new UserProfileRep();//Repository'mizi kullanabilmek için instance'ını aldık
        }

        public ActionResult RegisterNow()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterNow(AppUserVM apvm) //VM'in içindeki bütün property'leri kullanıcağımız için VM'i komple çektik
        {
            AppUser appUser = apvm.AppUser;
            UserProfile profile = apvm.UserProfile;

            if (_auRep.Any(x => x.UserName == appUser.UserName))
            {
                ViewBag.ZatenVar = "Kullanıcı ismi başka biri tarafından kullanılmaktadır";
                return View();
            }
            else if (_auRep.Any(x => x.Email == appUser.Email))
            {
                ViewBag.ZatenVar = "Belirtilen Email başka bir kullanıcı tarafından kullanılmaktadır";
                return View();
            }

            appUser.Password = DantexCryptex.Crypt(appUser.Password); //Kullanıcı şifreleri DB'de Crypted şekilde tutuluyor

            string gonderilecekMail = "Tebrikler hesabınız oluşturuldu. Hesabınızı aktif hale getirmek için https://localhost:44393/Register/Activation/" + appUser.ActivationCode + " linkine tıklayınız";

            MailService.Send(appUser.Email, body:gonderilecekMail, subject:"Hesap aktivasyonu");

            _auRep.Add(appUser); //Bire-Bir olduğu içi Profile ile, ilk AppUser kaydedilmeliki ID'si identity olarak oluşsun sonra da oluşan identity'i Profile'ın ID'sine atayıp birbirleriyle ilişkilendirelim.

            if (!string.IsNullOrEmpty(profile.FirstName) && !string.IsNullOrEmpty(profile.LastName) && !string.IsNullOrEmpty(profile.Country) && !string.IsNullOrEmpty(profile.Region) && !string.IsNullOrEmpty(profile.City) && !string.IsNullOrEmpty(profile.Address))
            {
                profile.ID = appUser.ID;//Profile'ı AppUser ile ilişkilendirdik
                _upRep.Add(profile);//Belirtilen koşullardan herhangi biri sağlanırsa, kullanıcının profili oluşturulur
            }

            return View("RegisterOk");
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

        public ActionResult Activation(Guid id)//Yolladığımız mail'de Actiovation/+appUser.ActivationCode+ bu ActivationCode'un bu sayfaya gelebilmesi için Guid tipin id parametresi açtık
        {
            AppUser aktifEdilecek = _auRep.FirstOrDefault(x => x.ActivationCode == id);

            if (aktifEdilecek == null)
            {
                TempData["HesapAktif"] = "Hesabınız bulunamadı";
                return RedirectToAction("Login", "Home");
            }

            aktifEdilecek.Active = true;
            _auRep.Update(aktifEdilecek);
            TempData["HesapAktifDegil"] = "Hesabınız aktif hale getirildi";
            return RedirectToAction("Login", "Home");

        }
    }
}