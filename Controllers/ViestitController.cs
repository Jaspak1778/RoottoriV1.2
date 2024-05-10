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
    public class ViestitController : Controller
    {
        private RoottoriDBEntities2 db = new RoottoriDBEntities2();

        // GET: Viestit
        public ActionResult Index()
        {
            var viestit = db.Viestit.OrderByDescending(v => v.ViestiId).ToList();
            return View(viestit);


        }


        public ActionResult Search(string searchTerm)
        {
            // Jaa hakusanojen merkkijono välilyöntien perusteella ja poista tyhjät merkkijonot
            string[] searchTerms = searchTerm.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Luo lista, johon tallennetaan löydetyt viestit
            List<Viestit> foundMessages = new List<Viestit>();

            // Etsi viestejä, jotka sisältävät jokaisen hakusanan
            foreach (string term in searchTerms)
            {
                var messages = db.Viestit.Where(m => m.Sisalto.Contains(term)).ToList();
                foundMessages.AddRange(messages);
            }

            // Poista mahdolliset duplikaatit
            var uniqueMessages = foundMessages.Distinct().ToList();

            // Tarkista, onko löydettyjä viestejä
            if (uniqueMessages.Count == 0)
            {
                // Jos viestejä ei löydy, palauta kaikki viestit järjestettynä ViestiId:n mukaan laskevassa järjestyksessä
                var allMessages = db.Viestit.OrderByDescending(v => v.ViestiId).ToList();
                return View("Index", allMessages);
            }
            else
            {
                // Jos viestejä löytyy, palauta ne Index2-näkymään
                return View("Index2", uniqueMessages);
            }
        }

        // GET: Viestit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viestit viestit = db.Viestit.Find(id);
            if (viestit == null)
            {
                return HttpNotFound();
            }
            return View(viestit);
        }

        // GET: Viestit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Viestit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ViestiId,Sisalto,Lahettaja,Aika")] Viestit viestit)
        {
            if (ModelState.IsValid)
            {
                db.Viestit.Add(viestit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viestit);
        }

        // GET: Viestit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viestit viestit = db.Viestit.Find(id);
            if (viestit == null)
            {
                return HttpNotFound();
            }
            return View(viestit);
        }

        // POST: Viestit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ViestiId,Sisalto,Lahettaja,Aika")] Viestit viestit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(viestit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viestit);
        }

        // GET: Viestit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viestit viestit = db.Viestit.Find(id);
            if (viestit == null)
            {
                return HttpNotFound();
            }
            return View(viestit);
        }

        // POST: Viestit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Viestit viestit = db.Viestit.Find(id);
            db.Viestit.Remove(viestit);
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