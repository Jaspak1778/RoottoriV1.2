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
    public class MalliE25RasetusController : Controller
    {
        private RoottoriDBEntities2 db = new RoottoriDBEntities2();

        // GET: MalliE25Rasetus
        public ActionResult Index()
        {
            /*var malliE25Rasetus = db.MalliE25Rasetus.Include(m => m.Karjet).Include(m => m.Leuat).Include(m => m.Magneetit).Include(m => m.Paletit).Include(m => m.Piirustukset).Include(m => m.Roottorit);
            return View(malliE25Rasetus.ToList());*/

            var roottori = db.Roottorit.Include(r => r.Koneet)
                             .FirstOrDefault(r => r.RoottoriID == 1001);

            return View(roottori);


        }

        // GET: MalliE25Rasetus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MalliE25Rasetus malliE25Rasetus = db.MalliE25Rasetus.Find(id);
            if (malliE25Rasetus == null)
            {
                return HttpNotFound();
            }
            return View(malliE25Rasetus);
        }

        // GET: MalliE25Rasetus/Create
        public ActionResult Create()
        {
            ViewBag.KarkiID = new SelectList(db.Karjet, "KarkiID", "KarkiMalli");
            ViewBag.LeukaID = new SelectList(db.Leuat, "LeukaID", "Leuat1");
            ViewBag.MagneettiID = new SelectList(db.Magneetit, "MagneettiID", "Magneetti");
            ViewBag.PalettiID = new SelectList(db.Paletit, "PalettiID", "Paletti");
            ViewBag.PiirustusID = new SelectList(db.Piirustukset, "PiirustusID", "Piirustusnro");
            ViewBag.RoottoriID = new SelectList(db.Roottorit, "RoottoriID", "Malli");
            return View();
        }

        // POST: MalliE25Rasetus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AsetusID,LeukaID,KarkiID,MagneettiID,PalettiID,PiirustusID,RoottoriID")] MalliE25Rasetus malliE25Rasetus)
        {
            if (ModelState.IsValid)
            {
                db.MalliE25Rasetus.Add(malliE25Rasetus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KarkiID = new SelectList(db.Karjet, "KarkiID", "KarkiMalli", malliE25Rasetus.KarkiID);
            ViewBag.LeukaID = new SelectList(db.Leuat, "LeukaID", "Leuat1", malliE25Rasetus.LeukaID);
            ViewBag.MagneettiID = new SelectList(db.Magneetit, "MagneettiID", "Magneetti", malliE25Rasetus.MagneettiID);
            ViewBag.PalettiID = new SelectList(db.Paletit, "PalettiID", "Paletti", malliE25Rasetus.PalettiID);
            ViewBag.PiirustusID = new SelectList(db.Piirustukset, "PiirustusID", "Piirustusnro", malliE25Rasetus.PiirustusID);
            ViewBag.RoottoriID = new SelectList(db.Roottorit, "RoottoriID", "Malli", malliE25Rasetus.RoottoriID);
            return View(malliE25Rasetus);
        }

        // GET: MalliE25Rasetus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MalliE25Rasetus malliE25Rasetus = db.MalliE25Rasetus.Find(id);
            if (malliE25Rasetus == null)
            {
                return HttpNotFound();
            }
            ViewBag.KarkiID = new SelectList(db.Karjet, "KarkiID", "KarkiMalli", malliE25Rasetus.KarkiID);
            ViewBag.LeukaID = new SelectList(db.Leuat, "LeukaID", "Leuat1", malliE25Rasetus.LeukaID);
            ViewBag.MagneettiID = new SelectList(db.Magneetit, "MagneettiID", "Magneetti", malliE25Rasetus.MagneettiID);
            ViewBag.PalettiID = new SelectList(db.Paletit, "PalettiID", "Paletti", malliE25Rasetus.PalettiID);
            ViewBag.PiirustusID = new SelectList(db.Piirustukset, "PiirustusID", "Piirustusnro", malliE25Rasetus.PiirustusID);
            ViewBag.RoottoriID = new SelectList(db.Roottorit, "RoottoriID", "Malli", malliE25Rasetus.RoottoriID);
            return View(malliE25Rasetus);
        }

        // POST: MalliE25Rasetus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AsetusID,LeukaID,KarkiID,MagneettiID,PalettiID,PiirustusID,RoottoriID")] MalliE25Rasetus malliE25Rasetus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(malliE25Rasetus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KarkiID = new SelectList(db.Karjet, "KarkiID", "KarkiMalli", malliE25Rasetus.KarkiID);
            ViewBag.LeukaID = new SelectList(db.Leuat, "LeukaID", "Leuat1", malliE25Rasetus.LeukaID);
            ViewBag.MagneettiID = new SelectList(db.Magneetit, "MagneettiID", "Magneetti", malliE25Rasetus.MagneettiID);
            ViewBag.PalettiID = new SelectList(db.Paletit, "PalettiID", "Paletti", malliE25Rasetus.PalettiID);
            ViewBag.PiirustusID = new SelectList(db.Piirustukset, "PiirustusID", "Piirustusnro", malliE25Rasetus.PiirustusID);
            ViewBag.RoottoriID = new SelectList(db.Roottorit, "RoottoriID", "Malli", malliE25Rasetus.RoottoriID);
            return View(malliE25Rasetus);
        }

        // GET: MalliE25Rasetus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MalliE25Rasetus malliE25Rasetus = db.MalliE25Rasetus.Find(id);
            if (malliE25Rasetus == null)
            {
                return HttpNotFound();
            }
            return View(malliE25Rasetus);
        }

        // POST: MalliE25Rasetus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MalliE25Rasetus malliE25Rasetus = db.MalliE25Rasetus.Find(id);
            db.MalliE25Rasetus.Remove(malliE25Rasetus);
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