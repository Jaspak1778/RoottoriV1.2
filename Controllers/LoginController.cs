using RoottoriV1._2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoottoriV1._2.Controllers
{   

    public class CheckSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserName"] == null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
    
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Logins LoginModel)
        {
            RoottoriDBEntities2 db = new RoottoriDBEntities2();
            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "In";
                Session["UserName"] = LoggedUser.UserName;
                return RedirectToAction("Index", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa --> Home/Index
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                return View("Login", "Home");
            }
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Endsession", "Home");
        }
    }
}