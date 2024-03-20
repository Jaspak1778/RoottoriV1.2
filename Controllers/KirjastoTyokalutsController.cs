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
    public class KirjastoTyokalutsController : Controller
    {
        private RoottoriDBEntities db = new RoottoriDBEntities();

        /*// GET: KirjastoTyokaluts
        public ActionResult Index()
        {
            return View(db.KirjastoTyokalut.ToList());
        }*/

        public ActionResult Index(string currentFilter1, string searchString1)
        {
            var tyokalut = from p in db.KirjastoTyokalut
                           select p;
            if (!String.IsNullOrEmpty(searchString1))
            {
                tyokalut = tyokalut.Where(p => p.TyokalunNimi.Contains(searchString1));
            }
            return View(tyokalut);
        }


        // GET: KirjastoTyokaluts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KirjastoTyokalut kirjastoTyokalut = db.KirjastoTyokalut.Find(id);
            if (kirjastoTyokalut == null)
            {
                return HttpNotFound();
            }
            return View(kirjastoTyokalut);
        }

        // GET: KirjastoTyokaluts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KirjastoTyokaluts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TyokaluID,TyokaluKategoriaID,TyokaluNro,TyokalunNimi,Pituus,Halkaisija,Pala,ImageLink,Lisatieto1,Lisatieto2,URL")] KirjastoTyokalut kirjastoTyokalut)
        {
            if (ModelState.IsValid)
            {
                db.KirjastoTyokalut.Add(kirjastoTyokalut);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kirjastoTyokalut);
        }

        // GET: KirjastoTyokaluts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KirjastoTyokalut kirjastoTyokalut = db.KirjastoTyokalut.Find(id);
            if (kirjastoTyokalut == null)
            {
                return HttpNotFound();
            }
            return View(kirjastoTyokalut);
        }

        // POST: KirjastoTyokaluts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TyokaluID,TyokaluKategoriaID,TyokaluNro,TyokalunNimi,Pituus,Halkaisija,Pala,ImageLink,Lisatieto1,Lisatieto2,URL")] KirjastoTyokalut kirjastoTyokalut)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kirjastoTyokalut).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kirjastoTyokalut);
        }

        // GET: KirjastoTyokaluts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KirjastoTyokalut kirjastoTyokalut = db.KirjastoTyokalut.Find(id);
            if (kirjastoTyokalut == null)
            {
                return HttpNotFound();
            }
            return View(kirjastoTyokalut);
        }

        // POST: KirjastoTyokaluts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KirjastoTyokalut kirjastoTyokalut = db.KirjastoTyokalut.Find(id);
            db.KirjastoTyokalut.Remove(kirjastoTyokalut);
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
