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
    public class MalliE25RiasetusController : Controller
    {
        private RoottoriDBEntities db = new RoottoriDBEntities();

        // GET: MalliE25Riasetus
        public ActionResult Index()
        {
            var malliE25Riasetus = db.MalliE25Riasetus.Include(m => m.Karjet).Include(m => m.Koneet).Include(m => m.Leuat).Include(m => m.Magneetit).Include(m => m.Paletit).Include(m => m.Piirustukset).Include(m => m.Roottorit);
            return View(malliE25Riasetus.ToList());
        }

        // GET: MalliE25Riasetus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MalliE25Riasetus malliE25Riasetus = db.MalliE25Riasetus.Find(id);
            if (malliE25Riasetus == null)
            {
                return HttpNotFound();
            }
            return View(malliE25Riasetus);
        }

        // GET: MalliE25Riasetus/Create
        public ActionResult Create()
        {
            ViewBag.KarkiID = new SelectList(db.Karjet, "KarkiID", "KarkiMalli");
            ViewBag.KoneID = new SelectList(db.Koneet, "KoneID", "Kone");
            ViewBag.LeukaID = new SelectList(db.Leuat, "LeukaID", "Leuat1");
            ViewBag.MagneettiID = new SelectList(db.Magneetit, "MagneettiID", "Magneetti");
            ViewBag.PalettiID = new SelectList(db.Paletit, "PalettiID", "Paletti");
            ViewBag.PiirustusID = new SelectList(db.Piirustukset, "PiirustusID", "Piirustusnro");
            ViewBag.RoottoriID = new SelectList(db.Roottorit, "RoottoriID", "Malli");
            return View();
        }

        // POST: MalliE25Riasetus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AsetusID,RoottoriID,KoneID,LeukaID,KarkiID,MagneettiID,PalettiID,PiirustusID")] MalliE25Riasetus malliE25Riasetus)
        {
            if (ModelState.IsValid)
            {
                db.MalliE25Riasetus.Add(malliE25Riasetus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KarkiID = new SelectList(db.Karjet, "KarkiID", "KarkiMalli", malliE25Riasetus.KarkiID);
            ViewBag.KoneID = new SelectList(db.Koneet, "KoneID", "Kone", malliE25Riasetus.KoneID);
            ViewBag.LeukaID = new SelectList(db.Leuat, "LeukaID", "Leuat1", malliE25Riasetus.LeukaID);
            ViewBag.MagneettiID = new SelectList(db.Magneetit, "MagneettiID", "Magneetti", malliE25Riasetus.MagneettiID);
            ViewBag.PalettiID = new SelectList(db.Paletit, "PalettiID", "Paletti", malliE25Riasetus.PalettiID);
            ViewBag.PiirustusID = new SelectList(db.Piirustukset, "PiirustusID", "Piirustusnro", malliE25Riasetus.PiirustusID);
            ViewBag.RoottoriID = new SelectList(db.Roottorit, "RoottoriID", "Malli", malliE25Riasetus.RoottoriID);
            return View(malliE25Riasetus);
        }

        // GET: MalliE25Riasetus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MalliE25Riasetus malliE25Riasetus = db.MalliE25Riasetus.Find(id);
            if (malliE25Riasetus == null)
            {
                return HttpNotFound();
            }
            ViewBag.KarkiID = new SelectList(db.Karjet, "KarkiID", "KarkiMalli", malliE25Riasetus.KarkiID);
            ViewBag.KoneID = new SelectList(db.Koneet, "KoneID", "Kone", malliE25Riasetus.KoneID);
            ViewBag.LeukaID = new SelectList(db.Leuat, "LeukaID", "Leuat1", malliE25Riasetus.LeukaID);
            ViewBag.MagneettiID = new SelectList(db.Magneetit, "MagneettiID", "Magneetti", malliE25Riasetus.MagneettiID);
            ViewBag.PalettiID = new SelectList(db.Paletit, "PalettiID", "Paletti", malliE25Riasetus.PalettiID);
            ViewBag.PiirustusID = new SelectList(db.Piirustukset, "PiirustusID", "Piirustusnro", malliE25Riasetus.PiirustusID);
            ViewBag.RoottoriID = new SelectList(db.Roottorit, "RoottoriID", "Malli", malliE25Riasetus.RoottoriID);
            return View(malliE25Riasetus);
        }

        // POST: MalliE25Riasetus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AsetusID,RoottoriID,KoneID,LeukaID,KarkiID,MagneettiID,PalettiID,PiirustusID")] MalliE25Riasetus malliE25Riasetus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(malliE25Riasetus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KarkiID = new SelectList(db.Karjet, "KarkiID", "KarkiMalli", malliE25Riasetus.KarkiID);
            ViewBag.KoneID = new SelectList(db.Koneet, "KoneID", "Kone", malliE25Riasetus.KoneID);
            ViewBag.LeukaID = new SelectList(db.Leuat, "LeukaID", "Leuat1", malliE25Riasetus.LeukaID);
            ViewBag.MagneettiID = new SelectList(db.Magneetit, "MagneettiID", "Magneetti", malliE25Riasetus.MagneettiID);
            ViewBag.PalettiID = new SelectList(db.Paletit, "PalettiID", "Paletti", malliE25Riasetus.PalettiID);
            ViewBag.PiirustusID = new SelectList(db.Piirustukset, "PiirustusID", "Piirustusnro", malliE25Riasetus.PiirustusID);
            ViewBag.RoottoriID = new SelectList(db.Roottorit, "RoottoriID", "Malli", malliE25Riasetus.RoottoriID);
            return View(malliE25Riasetus);
        }

        // GET: MalliE25Riasetus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MalliE25Riasetus malliE25Riasetus = db.MalliE25Riasetus.Find(id);
            if (malliE25Riasetus == null)
            {
                return HttpNotFound();
            }
            return View(malliE25Riasetus);
        }

        // POST: MalliE25Riasetus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MalliE25Riasetus malliE25Riasetus = db.MalliE25Riasetus.Find(id);
            db.MalliE25Riasetus.Remove(malliE25Riasetus);
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
