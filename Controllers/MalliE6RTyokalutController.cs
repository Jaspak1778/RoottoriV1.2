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
    public class MalliE6RTyokalutController : Controller
    {
        private RoottoriDBEntities2 db = new RoottoriDBEntities2();

        // GET: MalliE6RTyokalut
        public ActionResult Index()
        {
            var malliE6RTyokalut = db.MalliE6RTyokalut.Include(m => m.KirjastoTyokalut);
            return View(malliE6RTyokalut.ToList());
        }

        // GET: MalliE6RTyokalut/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MalliE6RTyokalut malliE6RTyokalut = db.MalliE6RTyokalut.Find(id);
            if (malliE6RTyokalut == null)
            {
                return HttpNotFound();
            }
            return View(malliE6RTyokalut);
        }

        // GET: MalliE6RTyokalut/Create
        public ActionResult Create()
        {
            ViewBag.TyokaluID = new SelectList(db.KirjastoTyokalut, "TyokaluID", "TyokalunNimi");
            return View();
        }

        // POST: MalliE6RTyokalut/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TyokaluPaikka,TyokaluID,Kesto,Paivitys")] MalliE6RTyokalut malliE6RTyokalut)
        {
            if (ModelState.IsValid)
            {
                db.MalliE6RTyokalut.Add(malliE6RTyokalut);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TyokaluID = new SelectList(db.KirjastoTyokalut, "TyokaluID", "TyokalunNimi", malliE6RTyokalut.TyokaluID);
            return View(malliE6RTyokalut);
        }

        // GET: MalliE6RTyokalut/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MalliE6RTyokalut malliE6RTyokalut = db.MalliE6RTyokalut.Find(id);
            if (malliE6RTyokalut == null)
            {
                return HttpNotFound();
            }
            ViewBag.TyokaluID = new SelectList(db.KirjastoTyokalut, "TyokaluID", "TyokalunNimi", malliE6RTyokalut.TyokaluID);
            return View(malliE6RTyokalut);
        }

        // POST: MalliE6RTyokalut/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TyokaluPaikka,TyokaluID,Kesto,Paivitys")] MalliE6RTyokalut malliE6RTyokalut)
        {
            if (ModelState.IsValid)
            {
                db.Entry(malliE6RTyokalut).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TyokaluID = new SelectList(db.KirjastoTyokalut, "TyokaluID", "TyokalunNimi", malliE6RTyokalut.TyokaluID);
            return View(malliE6RTyokalut);
        }

        // GET: MalliE6RTyokalut/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MalliE6RTyokalut malliE6RTyokalut = db.MalliE6RTyokalut.Find(id);
            if (malliE6RTyokalut == null)
            {
                return HttpNotFound();
            }
            return View(malliE6RTyokalut);
        }

        // POST: MalliE6RTyokalut/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MalliE6RTyokalut malliE6RTyokalut = db.MalliE6RTyokalut.Find(id);
            db.MalliE6RTyokalut.Remove(malliE6RTyokalut);
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
