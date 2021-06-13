using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
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
    public class CategoryController : Controller
    {
        CategoryRep _cRep;//Kısayollarımı kullanmak için Repository class'ımı kullanıyorum

        public CategoryController()
        {
            _cRep = new CategoryRep();//Repository'mizi kullanabilmek için instance'ını aldık
        }

        public ActionResult CategoryList(int? id)//id'li hali için ayrı bir action açmaktansa ternary if ile tek action'da 2 ayrı request'e de cevap vermiş oldum
        {
            //CategoryList'e Get request'i olduğunda id parametresi null ise bütün kategorileri getir, null değilse istenen kategoriyi getir
            CategoryVM cvm = new CategoryVM
            {
                Categories = id == null ? _cRep.GetActives() : _cRep.Where(x => x.ID == id)
            };

            return View(cvm);//VM yöntemiyde View'a model yolladık
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            _cRep.Add(category);

            return RedirectToAction("CategoryList");
        }

        public ActionResult UpdateCategory(int id)
        {
            CategoryVM cvm = new CategoryVM
            {
                Category = _cRep.Find(id)
            };
            return View(cvm);
        }

        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {
            _cRep.Update(category);

            return RedirectToAction("CategoryList");
        }

        public ActionResult DeleteCategory(int id)
        {
            _cRep.Delete(_cRep.Find(id));

            return RedirectToAction("CategoryList");
        }
    }
}