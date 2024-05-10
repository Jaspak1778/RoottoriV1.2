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
    public class LeuatController : Controller
    {
        private RoottoriDBEntities2 db = new RoottoriDBEntities2();

        // GET: Leuat
        public ActionResult Index()
        {
            return View(db.Leuat.ToList());
        }

        // GET: Leuat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leuat leuat = db.Leuat.Find(id);
            if (leuat == null)
            {
                return HttpNotFound();
            }
            return View(leuat);
        }

        // GET: Leuat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Leuat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeukaID,Leuat1,LeukaAsetus,Leukapaine,ImageLink")] Leuat leuat)
        {
            if (ModelState.IsValid)
            {
                db.Leuat.Add(leuat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leuat);
        }

        // GET: Leuat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leuat leuat = db.Leuat.Find(id);
            if (leuat == null)
            {
                return HttpNotFound();
            }
            return View(leuat);
        }

        // POST: Leuat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LeukaID,Leuat1,LeukaAsetus,Leukapaine,ImageLink")] Leuat leuat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leuat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leuat);
        }

        // GET: Leuat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leuat leuat = db.Leuat.Find(id);
            if (leuat == null)
            {
                return HttpNotFound();
            }
            return View(leuat);
        }

        // POST: Leuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Leuat leuat = db.Leuat.Find(id);
            db.Leuat.Remove(leuat);
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
