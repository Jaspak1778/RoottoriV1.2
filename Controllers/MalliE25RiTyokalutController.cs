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
        private RoottoriDBEntities2 db = new RoottoriDBEntities2();

        // GET: MalliE25RiTyokalut
        public ActionResult Index()
        {
            var malliE25RiTyokalut = db.MalliE25RiTyokalut.Include(m => m.KirjastoTyokalut);
            return View(malliE25RiTyokalut.ToList());
        }

        // GET: MalliE25RiTyokalut/Details/5
        public ActionResult Details(int? id)
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
            return View(malliE25RiTyokalut);
        }

        // GET: MalliE25RiTyokalut/Create
        public ActionResult Create()
        {
            ViewBag.TyokaluID = new SelectList(db.KirjastoTyokalut, "TyokaluID", "TyokalunNimi","Kesto");
            return View();
        }

        // POST: MalliE25RiTyokalut/Create
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
            ViewBag.TyokaluID = new SelectList(db.KirjastoTyokalut, "TyokaluID", "TyokalunNimi,Kesto", malliE25RiTyokalut.TyokaluID);
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
        public ActionResult Delete(int? id)
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
            return View(malliE25RiTyokalut);
        }

        // POST: MalliE25RiTyokalut/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MalliE25RiTyokalut malliE25RiTyokalut = db.MalliE25RiTyokalut.Find(id);
            db.MalliE25RiTyokalut.Remove(malliE25RiTyokalut);
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
