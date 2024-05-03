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
    public class RoottoritController : Controller
    {
        private readonly RoottoriDBEntities2 db = new RoottoriDBEntities2();

        // GET: Roottorit
        public ActionResult Index()
        {
            var roottorit = db.Roottorit.Include(r => r.Koneet).Include(r => r.Karjet).Include(r => r.Leuat).Include(r => r.Magneetit).Include(r => r.Paletit).Include(r => r.Piirustukset);
            return View(roottorit.ToList());
        }

        // GET: Asetus näkymä E25R mallille
        public ActionResult E25R()
        {
            var roottori = db.Roottorit
                             .Include(r => r.Koneet)
                             .FirstOrDefault(r => r.RoottoriID == 1001); // ID 1001 = E25R Roottorit taulussa, saa olla kova koodattuna koska nämä ovat projektin pysyvät mallit

            return View(roottori);
        }

        // GET: Asetus näkymä E6R mallille - tähän controllerit muille asetusnäkymille
        public ActionResult E6R()
        {
            var roottori = db.Roottorit
                             .Include(r => r.Koneet)
                             .FirstOrDefault(r => r.RoottoriID == 1000); // ID 1000 = E6R Roottorit taulussa, saa olla kova koodattuna koska nämä ovat projektin pysyvät mallit

            return View(roottori);
        }


        // GET: Asetus näkymä E25Ri mallille - tähän controllerit muille asetusnäkymille
        public ActionResult E25Ri()
        {
            var roottori = db.Roottorit
                             .Include(r => r.Koneet)
                             .FirstOrDefault(r => r.RoottoriID == 1002); // ID 1002 = E25Ri Roottorit taulussa, saa olla kova koodattuna koska nämä ovat projektin pysyvät mallit

            return View(roottori);
        }

        // GET: Roottorit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roottorit roottorit = db.Roottorit.Find(id);
            if (roottorit == null)
            {
                return HttpNotFound();
            }
            return View(roottorit);
        }

        // GET: Roottorit/Create
        public ActionResult Create()
        {
            ViewBag.KoneID = new SelectList(db.Koneet, "KoneID", "Kone");
            ViewBag.KarkiID = new SelectList(db.Karjet, "KarkiID", "KarkiMalli");
            ViewBag.LeukaID = new SelectList(db.Leuat, "LeukaID", "LeukaAsetus");
            ViewBag.MagneettiID = new SelectList(db.Magneetit, "MagneettiID", "Magneetti");
            ViewBag.PalettiID = new SelectList(db.Paletit, "PalettiID", "Paletti");
            ViewBag.PiirustusID = new SelectList(db.Piirustukset, "PiirustusID", "Piirustusnro");
            return View();
        }

        // POST: Roottorit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoottoriID,Malli,RaakaAine,Pituus,Halkaisija,Kiinnityspinta,OhjNrot,KoneID,KarkiID,LeukaID,MagneettiID,PalettiID,PiirustusID")] Roottorit roottorit)
        {
            if (ModelState.IsValid)
            {
                db.Roottorit.Add(roottorit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KoneID = new SelectList(db.Koneet, "KoneID", "Kone", roottorit.KoneID);
            ViewBag.KarkiID = new SelectList(db.Karjet, "KarkiID", "KarkiMalli", roottorit.KarkiID);
            ViewBag.LeukaID = new SelectList(db.Leuat, "LeukaID", "LeuatAsetus", roottorit.LeukaID);
            ViewBag.MagneettiID = new SelectList(db.Magneetit, "MagneettiID", "Magneetti", roottorit.MagneettiID);
            ViewBag.PalettiID = new SelectList(db.Paletit, "PalettiID", "Paletti", roottorit.PalettiID);
            ViewBag.PiirustusID = new SelectList(db.Piirustukset, "PiirustusID", "Piirustusnro", roottorit.PiirustusID);
            return View(roottorit);
        }

        // GET: Roottorit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roottorit roottorit = db.Roottorit.Find(id);
            if (roottorit == null)
            {
                return HttpNotFound();
            }
            ViewBag.KoneID = new SelectList(db.Koneet, "KoneID", "Kone", roottorit.KoneID);
            ViewBag.KarkiID = new SelectList(db.Karjet, "KarkiID", "KarkiMalli", roottorit.KarkiID);
            ViewBag.LeukaID = new SelectList(db.Leuat, "LeukaID", "LeukaAsetus", roottorit.LeukaID);
            ViewBag.MagneettiID = new SelectList(db.Magneetit, "MagneettiID", "Magneetti", roottorit.MagneettiID);
            ViewBag.PalettiID = new SelectList(db.Paletit, "PalettiID", "Paletti", roottorit.PalettiID);
            ViewBag.PiirustusID = new SelectList(db.Piirustukset, "PiirustusID", "Piirustusnro", roottorit.PiirustusID);
            return View(roottorit);
        }

        // POST: Roottorit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoottoriID,Malli,RaakaAine,Pituus,Halkaisija,Kiinnityspinta,OhjNrot,KoneID,KarkiID,LeukaID,MagneettiID,PalettiID,PiirustusID")] Roottorit roottorit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roottorit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KoneID = new SelectList(db.Koneet, "KoneID", "Kone", roottorit.KoneID);
            ViewBag.KarkiID = new SelectList(db.Karjet, "KarkiID", "KarkiMalli", roottorit.KarkiID);
            ViewBag.LeukaID = new SelectList(db.Leuat, "LeukaID", "LeukaAsetus", roottorit.LeukaID);
            ViewBag.MagneettiID = new SelectList(db.Magneetit, "MagneettiID", "Magneetti", roottorit.MagneettiID);
            ViewBag.PalettiID = new SelectList(db.Paletit, "PalettiID", "Paletti", roottorit.PalettiID);
            ViewBag.PiirustusID = new SelectList(db.Piirustukset, "PiirustusID", "Piirustusnro", roottorit.PiirustusID);
            return View(roottorit);
        }

        // GET: Roottorit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roottorit roottorit = db.Roottorit.Find(id);
            if (roottorit == null)
            {
                return HttpNotFound();
            }
            return View(roottorit);
        }

        // POST: Roottorit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Roottorit roottorit = db.Roottorit.Find(id);
            db.Roottorit.Remove(roottorit);
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