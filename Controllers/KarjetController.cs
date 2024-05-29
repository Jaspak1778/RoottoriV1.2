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
    public class KarjetController : Controller
    {
        private RoottoriDBEntities2 db = new RoottoriDBEntities2();
        //Luotu Karjet Controller muokkauksen parantamiseksi@Toni
        // GET: Karjet
        public ActionResult Index()
        {
            return View(db.Karjet.ToList());
        }

        // GET: Karjet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Karjet karjet = db.Karjet.Find(id);
            if (karjet == null)
            {
                return HttpNotFound();
            }
            return View(karjet);
        }

        // GET: Karjet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Karjet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KarkiID,KarkiMalli,ImageLink")] Karjet karjet)
        {
            if (ModelState.IsValid)
            {
                db.Karjet.Add(karjet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(karjet);
        }

        // GET: Karjet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Karjet karjet = db.Karjet.Find(id);
            if (karjet == null)
            {
                return HttpNotFound();
            }
            return View(karjet);
        }

        // POST: Karjet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KarkiID,KarkiMalli,ImageLink")] Karjet karjet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(karjet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(karjet);
        }

        // GET: Karjet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Karjet karjet = db.Karjet.Find(id);
            if (karjet == null)
            {
                return HttpNotFound();
            }
            return View(karjet);
        }

        // POST: Karjet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Karjet karjet = db.Karjet.Find(id);
            db.Karjet.Remove(karjet);
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
