 using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FilmPortali.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        AdminManager manager = new AdminManager(new EfAdminDal());
        
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin p)
        {
            if(manager.Authentication(p))
            {
                FormsAuthentication.SetAuthCookie(p.AdminUsername, false);
                Session["AdminUsername"] = p.AdminUsername.ToString();
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View();
            }
            
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}