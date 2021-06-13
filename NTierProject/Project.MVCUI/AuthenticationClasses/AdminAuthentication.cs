using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.AuthenticationClasses
{
    public class AdminAuthentication : AuthorizeAttribute
    {
        //İstediğim controller veya action'a giriş için Admin koşulu koymamı sağlayan class
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["admin"] != null)
            {
                return true; //Geçişe izin verdik
            }
            httpContext.Session["hata"] = "Bu sayfaya erişim izniniz bulunmamaktadır";
            httpContext.Response.Redirect("/Home/Login"); //Admin değilse belritilen sayfaya yönlendirip false çevir
            return false;
        }
    }
}