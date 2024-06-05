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
    public class KirjastoTyokalutController : Controller
    {
        private readonly RoottoriDBEntities2 db = new RoottoriDBEntities2();

        /*// GET: KirjastoTyokaluts
        public ActionResult Index()
        {
            return View(db.KirjastoTyokalut.ToList());
        }*/

        /*Toteutettu hakutoiminto työkaluille @Jani*/
        public ActionResult Index(string searchString1)
        {
            var tyokalut = from p in db.KirjastoTyokalut
                           select p;
            if (!String.IsNullOrEmpty(searchString1))
            {
                tyokalut = tyokalut.Where(p => p.TyokalunNimi.Contains(searchString1));
            }
            return View(tyokalut);
        }

        /*Tehty lajittelu IDn perusteella, konekohtaiset työkalut. Voidaan distributoida sittemmin suunnittelijan näkymästä halutulle koneelle @Jani*/
        // GET: KoneKohtTyokalut
        public ActionResult Mazak400()
        {
            var mazak400 = from t in db.KirjastoTyokalut
                           where t.KoneID == 1000
                           select t;

            return View(mazak400.ToList());
        }

        public ActionResult Mazaki500()
        {
            var mazak400 = from t in db.KirjastoTyokalut
                           where t.KoneID == 1001
                           select t;

            return View(mazak400.ToList());
        }

        // GET: KirjastoTyokalut/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KirjastoTyokalut kirjastoTyokalut = db.KirjastoTyokalut.Find(id);
            if (kirjastoTyokalut == null)
            {
                return HttpNotFound();
            }
            return View(kirjastoTyokalut);
        }

        // GET: KirjastoTyokalut/Create
        [CheckSession]
        public ActionResult Create(string returnurl)
        {
            
            /*ViewBag.Returnurl = returnurl;   //Debug koodi returnurl @Jani*/
            ViewBag.KoneID = new SelectList(db.Koneet, "KoneID", "Kone");
            return View();
        }

        // POST: KirjastoTyokalut/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TyokaluID,TyokaluKategoriaID,TyokaluNro,TyokalunNimi,Pituus,Halkaisija,Pala,ImageLink,Lisatieto1,KoneID")] KirjastoTyokalut kirjastoTyokalut, string returnurl)
        {
            if (ModelState.IsValid)
            {
                db.KirjastoTyokalut.Add(kirjastoTyokalut);
                db.SaveChanges();
                string redirectUrl = returnurl ?? Url.Action("Index", "KirjastoTyokalut"); //virheenkäsittely jos returnurl ei ole kelvollinen @Jani
                return Redirect(redirectUrl);
                /*Redirectointi*/
            }

            ViewBag.KoneID = new SelectList(db.Koneet, "KoneID", "Kone", kirjastoTyokalut.KoneID);
            return View(kirjastoTyokalut);
        }

        // GET: KirjastoTyokalut/Edit/5
        [CheckSession]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KirjastoTyokalut kirjastoTyokalut = db.KirjastoTyokalut.Find(id);
            if (kirjastoTyokalut == null)
            {
                return HttpNotFound();
            }
            ViewBag.KoneID = new SelectList(db.Koneet, "KoneID", "Kone", kirjastoTyokalut.KoneID);
            return View(kirjastoTyokalut);
        }

        // POST: KirjastoTyokalut/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TyokaluID,TyokaluKategoriaID,TyokaluNro,TyokalunNimi,Pituus,Halkaisija,Pala,ImageLink,Lisatieto1,KoneID")] KirjastoTyokalut kirjastoTyokalut)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kirjastoTyokalut).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KoneID = new SelectList(db.Koneet, "KoneID", "Kone", kirjastoTyokalut.KoneID);
            return View(kirjastoTyokalut);
        }

        // GET: KirjastoTyokalut/Delete/5
        [CheckSession]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KirjastoTyokalut kirjastoTyokalut = db.KirjastoTyokalut.Find(id);

            if (kirjastoTyokalut == null)
            {
                return HttpNotFound();
            }

            //käyttäjän opastus jos työkalu on jaettu toiseen tauluun @Jani
            bool AnyFind = db.MalliE25RiTyokalut.Any(row => row.TyokaluID == id);
            if (AnyFind)

            {   
                ViewBag.MalliPolku = "MalliE25RiTyokalut";
                ViewBag.estapoisto = true;
                ViewBag.Kehoitus = "Työkalu on osoitettu mallille E25Ri, poista työkalu ensin mallinäkymästä ja jatka sitten poistoa kirjaston puolelta.";
                return View(kirjastoTyokalut);
            }

            AnyFind = db.MalliE25RTyokalut.Any(row => row.TyokaluID == id);
            if (AnyFind)
            {
                ViewBag.MalliPolku = "MalliE25RTyokalut";
                ViewBag.estapoisto = true;
                ViewBag.Kehoitus = "Työkalu on osoitettu mallille E25R, poista työkalu ensin mallinäkymästä ja jatka sitten poistoa kirjaston puolelta.";
                return View(kirjastoTyokalut);
            }
            AnyFind = db.MalliE6RTyokalut.Any(row => row.TyokaluID == id);
            if (AnyFind)
            {
                ViewBag.MalliPolku = "MalliE6RTyokalut";
                ViewBag.estapoisto = true;
                ViewBag.Kehoitus = "Työkalu on osoitettu mallille E6R, poista työkalu ensin mallinäkymästä ja jatka sitten poistoa kirjaston puolelta.";
                return View(kirjastoTyokalut);
            }
            return View(kirjastoTyokalut);
        }

        // POST: KirjastoTyokalut/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KirjastoTyokalut kirjastoTyokalut = db.KirjastoTyokalut.Find(id);
            db.KirjastoTyokalut.Remove(kirjastoTyokalut);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Malli(int tkid)
        {
            TempData["id"] = tkid;
            return RedirectToAction("Create", "MalliE25ERTyokalut");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}