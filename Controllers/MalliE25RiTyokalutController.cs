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
    public class MalliE25RiTyokalutController : Controller
    {   
        //Alustaa tietokantayhteyden
        private readonly RoottoriDBEntities2 db = new RoottoriDBEntities2();

        // GET: MalliE25RiTyokalut
        // Palauttaa listanäkymän, jossa kaikki MalliE25RiTyokalut-tietueet
        public ActionResult Index()
        {
            var malliE25RiTyokalut = db.MalliE25RiTyokalut.Include(m => m.KirjastoTyokalut);
            return View(malliE25RiTyokalut.ToList());
        }

        // GET: MalliE25RiTyokalut/Details/5
        // Näyttää yksityiskohdat yksittäisestä työkalusta
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tool = db.MalliE25RiTyokalut.Find(id);
            if (tool == null)
            {
                return HttpNotFound();
            }
            // Tarkistetaan, onko kyseessä AJAX-pyyntö
            if (Request.IsAjaxRequest())
            {
                // Palautetaan osittaisnäkymä AJAX-pyyntöä varten
                return PartialView("_DetailsPartial", tool);  
            }
            return View(tool);
        }

        // GET: MalliE25RiTyokalut/Create
        // Näyttää lomakkeen uuden työkalun lisäämiseksi
        public ActionResult Create(string searchString1)
        {
            var valitut = db.MalliE25RiTyokalut.Select(t => t.TyokaluID).ToList();                           //Lajittelu jolla estetään duplikaattien lisääminen @Jani
            var tyokalut = db.KirjastoTyokalut.Where(k => !valitut.Contains(k.TyokaluID)).ToList();
            if (!String.IsNullOrEmpty(searchString1))
            {
                tyokalut = tyokalut.Where(x => x.TyokalunNimi.Contains(searchString1)).ToList();
            }
            return View(tyokalut);

        }

        // POST: MalliE25RiTyokalut/Create
        // Tallentaa uuden työkalun tietokantaan
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TyokaluPaikka,TyokaluID,Kesto")] MalliE25RiTyokalut malliE25RiTyokalut)
        {
            if (ModelState.IsValid)
            {
                db.MalliE25RiTyokalut.Add(malliE25RiTyokalut);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TyokaluID = new SelectList(db.KirjastoTyokalut, "TyokaluID", "TyokalunNimi", malliE25RiTyokalut.TyokaluID);
            return View(malliE25RiTyokalut);
        }

        // GET: MalliE25RiTyokalut/Edit/5
        // Näyttää muokkauslomakkeen yksittäiselle työkalulle
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MalliE25RiTyokalut malliE25RiTyokalut = db.MalliE25RiTyokalut.Find(id);
            if (malliE25RiTyokalut == null)
            {
                return HttpNotFound();
            }

            ViewBag.TyokaluID = new SelectList(db.KirjastoTyokalut, "TyokaluID", "TyokaluNro", malliE25RiTyokalut.TyokaluID);

            // Tarkistetaan, onko kyseessä AJAX-pyyntö
            if (Request.IsAjaxRequest())
            {
                // Palautetaan osittaisnäkymä AJAX-pyyntöä varten
                return PartialView("_EditPartial", malliE25RiTyokalut);
            }

            // Palautetaan tavallinen näkymä, jos ei ole AJAX-pyyntö
            return View(malliE25RiTyokalut);
        }

        // POST: MalliE25RiTyokalut/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TyokaluPaikka,TyokaluID,Kesto")] MalliE25RiTyokalut malliE25RiTyokalut)
        {
            if (ModelState.IsValid)
            {
                db.Entry(malliE25RiTyokalut).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TyokaluID = new SelectList(db.KirjastoTyokalut, "TyokaluID", "TyokalunNimi", malliE25RiTyokalut.TyokaluID);
            return View(malliE25RiTyokalut);
        }



        // GET: MalliE25RiTyokalut/Delete/5
        // Näyttää poistolomakkeen yksittäiselle työkalulle
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tool = db.MalliE25RiTyokalut.Find(id);
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

        // POST: MalliE25RiTyokalut/Delete/5
        // Suorittaa yksittäisen työkalun poiston tietokannasta
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MalliE25RiTyokalut malliE25RiTyokalut = db.MalliE25RiTyokalut.Find(id);
            db.MalliE25RiTyokalut.Remove(malliE25RiTyokalut);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Kuittaa(int tyokaluPaikka)
        {
            // Tässä vaiheessa voit esimerkiksi asettaa kuitatun ominaisuuden arvon trueksi.
            // Voit myös lähettää tämän tiedon edelleen näkymälle, jos haluat näyttää käyttäjälle kuitatun viestin.

            // Esimerkki:
            MalliE25RTyokalut malliE25RiKuittaus = db.MalliE25RTyokalut.Find(tyokaluPaikka);
            if (malliE25RiKuittaus != null)
            {
                malliE25RiKuittaus.Kuitattu = true;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // Vapauttaa tietokantayhteyden resurssit
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
