using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RoottoriV1._2.Models;


namespace RoottoriV1._2.Controllers
{
    public class HomeController : Controller
    {
        private readonly RoottoriDBEntities2 db = new RoottoriDBEntities2();
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult Login(string returnurl)
        {
            ViewBag.ReturnUrl = returnurl;
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(string returnurl,Logins LoginModel)
        {
            RoottoriDBEntities2 db = new RoottoriDBEntities2();
            //Haetaan käyttäjän/Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ -kyselyllä
            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "In";
                Session["UserName"] = LoggedUser.UserName;
                Session["LoginID"] = LoggedUser.LoginID;
                string redirectUrl = returnurl;
                return Redirect(redirectUrl);

            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                return View("Index", "Home");
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction("EndSession", "Home");
        }
        public ActionResult Endsession()
        {
            Session.Abandon();
            Session.Clear();
            ViewBag.LoggedOut = "Olet kirjautunut ulos järjestelmästä.";
            return View();
        }
    }

}