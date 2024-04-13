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
        private RoottoriDBEntities2 db = new RoottoriDBEntities2();
        public ActionResult Index()
        {
            var roottorit = db.Roottorit.Include(r => r.Koneet)
                                        .Include(r => r.Karjet)
                                        .Include(r => r.Leuat)
                                        .Include(r => r.Magneetit)
                                        .Include(r => r.Paletit)
                                        .Include(r => r.Piirustukset).ToList();

            return View("~/Views/Roottorit/Index.cshtml", roottorit);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View("~/Views/Roottorit/Index.cshtml");
            //return View("~/Views/Roottorit/Index.cshtml", roottorit);??????????????
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}