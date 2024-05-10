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
    public class MagneetitController : Controller
    {
        private RoottoriDBEntities2 db = new RoottoriDBEntities2();

        // GET: Magneetit
        public ActionResult Index()
        {
            return View(db.Magneetit.ToList());
        }

        // GET: Magneetit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magneetit magneetit = db.Magneetit.Find(id);
            if (magneetit == null)
            {
                return HttpNotFound();
            }
            return View(magneetit);
        }

        // GET: Magneetit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Magneetit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MagneettiID,Magneetti")] Magneetit magneetit)
        {
            if (ModelState.IsValid)
            {
                db.Magneetit.Add(magneetit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(magneetit);
        }

        // GET: Magneetit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magneetit magneetit = db.Magneetit.Find(id);
            if (magneetit == null)
            {
                return HttpNotFound();
            }
            return View(magneetit);
        }

        // POST: Magneetit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MagneettiID,Magneetti")] Magneetit magneetit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(magneetit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(magneetit);
        }

        // GET: Magneetit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magneetit magneetit = db.Magneetit.Find(id);
            if (magneetit == null)
            {
                return HttpNotFound();
            }
            return View(magneetit);
        }

        // POST: Magneetit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Magneetit magneetit = db.Magneetit.Find(id);
            db.Magneetit.Remove(magneetit);
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
