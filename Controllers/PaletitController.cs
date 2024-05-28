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
    public class PaletitController : Controller
    {
        private RoottoriDBEntities2 db = new RoottoriDBEntities2();

        // GET: Paletit
        public ActionResult Index()
        {
            return View(db.Paletit.ToList());
        }

        // GET: Paletit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paletit paletit = db.Paletit.Find(id);
            if (paletit == null)
            {
                return HttpNotFound();
            }
            return View(paletit);
        }

        // GET: Paletit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paletit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PalettiID,Paletti")] Paletit paletit)
        {
            if (ModelState.IsValid)
            {
                db.Paletit.Add(paletit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paletit);
        }

        // GET: Paletit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paletit paletit = db.Paletit.Find(id);
            if (paletit == null)
            {
                return HttpNotFound();
            }
            return View(paletit);
        }

        // POST: Paletit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PalettiID,Paletti")] Paletit paletit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paletit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paletit);
        }

        

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paletit paletit = db.Paletit.Find(id);
            if (paletit == null)
            {
                return HttpNotFound();
            }

            // Tarkistetaan, onko paletti liitetty johonkin roottoriin
            var roottorit = db.Roottorit.Where(r => r.PalettiID == id).ToList();
            if (roottorit.Any())
            {
                ViewBag.MalliPolku = "Roottorit";
                ViewBag.estapoisto = true;
                ViewBag.Kehoitus = $"Paletti on liitetty seuraaviin malleihin: {string.Join(", ", roottorit.Select(r => r.Malli))}.Lisää ensin uusi Paletti malli Muokkaus näkymässä, ja siirry sen jälkeen poistamaan.";
                return View(paletit);
            }

            return View(paletit);
        }

        // POST: Paletit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paletit paletit = db.Paletit.Find(id);

            // Tarkistetaan vielä, ettei paletti ole liitetty mihinkään roottoriin
            var roottorit = db.Roottorit.Where(r => r.PalettiID == id).ToList();
            if (roottorit.Any())
            {
                // Paletti on yhä liitetty roottoreihin, näytetään virheilmoitus
                ViewBag.MalliPolku = "Roottorit";
                ViewBag.estapoisto = true;
                ViewBag.Kehoitus = $"Paletti on liitetty seuraaviin malleihin: {string.Join(", ", roottorit.Select(r => r.Malli))}. Poista ensin liitokset näistä malleista ennen kuin jatkat poistoa.";
                return View("Delete", paletit);
            }

            db.Paletit.Remove(paletit);
            db.SaveChanges();
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
