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
    public class MalliE25RTyokalutController : Controller
    {
        private RoottoriDBEntities2 db = new RoottoriDBEntities2();

        // GET: MalliE25RTyokalut
        public ActionResult Index() 
        {   
            var tyokalut = db.MalliE25RTyokalut.OrderBy(t => t.KirjastoTyokalut.TyokaluNro).ToList();
            return View(tyokalut);
        }

        // GET: MalliE25RTyokalut/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tool = db.MalliE25RTyokalut.Find(id);
            if (tool == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_DetailsPartial", tool);
            }
            return View(tool);
        }


        // GET: MalliE25RTyokalut/Create
        // Näyttää lomakkeen uuden työkalun lisäämiseksi @Jani
        public ActionResult Create(string searchString1)
        {   
            var valitut = db.MalliE25RTyokalut.Select(t => t.TyokaluID).ToList();                           //Lajittelu jolla estetään duplikaattien lisääminen @Jani
            var tyokalut = db.KirjastoTyokalut.Where(k => !valitut.Contains(k.TyokaluID)).ToList();
            if (!String.IsNullOrEmpty(searchString1))
            {
                tyokalut = tyokalut.Where(x => x.TyokalunNimi.Contains(searchString1)).ToList();
            }
            return View(tyokalut);
       
        }

        // POST: MalliE25RTyokalut/Create
        /* Tallentaa uuden työkalun mallikohtaiseen tietokantatauluun, ei lisää uutta työkalua inventaarioon. @Jani */
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TyokaluID,Kesto")] MalliE25RTyokalut malliE25RTyokalut)
        {
            if (ModelState.IsValid)
            {
                db.MalliE25RTyokalut.Add(malliE25RTyokalut);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TyokaluID = new SelectList(db.KirjastoTyokalut, "TyokaluID", "TyokalunNimi", malliE25RTyokalut.TyokaluID);
            return View(malliE25RTyokalut);
        }

        // GET: MalliE25RTyokalut/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MalliE25RTyokalut malliE25RTyokalut = db.MalliE25RTyokalut.Find(id);
            if (malliE25RTyokalut == null)
            {
                return HttpNotFound();
            }

            ViewBag.TyokaluID = new SelectList(db.KirjastoTyokalut, "TyokaluID", "TyokaluNro", malliE25RTyokalut.TyokaluID);

            // Tarkistetaan, onko kyseessä AJAX-pyyntö
            if (Request.IsAjaxRequest())
            {
                // Palautetaan osittaisnäkymä AJAX-pyyntöä varten
                return PartialView("_EditPartial", malliE25RTyokalut);
            }

            // Palautetaan tavallinen näkymä, jos ei ole AJAX-pyyntö
            return View(malliE25RTyokalut);
        }


        // POST: MalliE25RTyokalut/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TyokaluPaikka,TyokaluID,Kesto,")] MalliE25RTyokalut malliE25RTyokalut)
        {
            if (ModelState.IsValid)
            {
                db.Entry(malliE25RTyokalut).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TyokaluID = new SelectList(db.KirjastoTyokalut, "TyokaluID", "TyokalunNimi", malliE25RTyokalut.TyokaluID);
            return View(malliE25RTyokalut);
        }

        // GET: MalliE25RTyokalut/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tool = db.MalliE25RTyokalut.Find(id);
            if (tool == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
            {

                return PartialView("_DeletePartial", tool);
            }

            return View(tool);
        }


        // POST: MalliE25RTyokalut/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MalliE25RTyokalut malliE25RTyokalut = db.MalliE25RTyokalut.Find(id);
            db.MalliE25RTyokalut.Remove(malliE25RTyokalut);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Kuittaa(int tyokaluPaikka)
        {
            // Tässä vaiheessa voit esimerkiksi asettaa kuitatun ominaisuuden arvon trueksi.
            // Voit myös lähettää tämän tiedon edelleen näkymälle, jos haluat näyttää käyttäjälle kuitatun viestin.

            // Esimerkki:
            MalliE25RTyokalut malliE25RKuittaa = db.MalliE25RTyokalut.Find(tyokaluPaikka);
            if (malliE25RKuittaa != null)
            {
                malliE25RKuittaa.Kuitattu = true;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
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
