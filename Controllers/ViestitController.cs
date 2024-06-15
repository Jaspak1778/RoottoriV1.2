using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Newtonsoft.Json;
using RoottoriV1._2.Models;
using RoottoriV1._2.ViewModel;

namespace RoottoriV1._2.Controllers
{
    public class ViestitController : Controller
    {
        private RoottoriDBEntities2 db = new RoottoriDBEntities2();

        // GET: Viestit
        //TODO: Viestit luokkaan lisäyksiä tuleva tai menevä viesti
        public ActionResult Index(string searchTerm)  //Viesti on JSON muodossa
        {

            //haetaan viestit, sisätlö JSON muodossa, viestin sisältöön on korvamerkitty laite tai voidaan muuttaa IP osoitteksi myöhemmin, kumpi on parempi @Jani
            ViewBag.Host = Environment.MachineName.ToString();
            
            var viestit = db.Viestit.OrderByDescending(v => v.ViestiId).ToList();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                viestit = viestit.FindAll(v => v.Sisalto.Contains(searchTerm));
            }

            foreach (var viesti in viestit)
            {
                try
                {
                    var sisaltoData = JsonConvert.DeserializeObject<SisaltoModel>(viesti.Sisalto);
                    viesti.Message = sisaltoData.Message;
                    viesti.Laite = sisaltoData.Laite;
                }
                catch (JsonReaderException ex)
                {
                    //virheen käsittely tähän
                }

            }

            return View(viestit);
        }
        // TODO: Edit ja Haku toiminnot
        //Taustatoiminto viesti liikenne notifikaatiot sekä tuleva ja lähtevän viestin logiikka @Jani
        public ActionResult ViestitService()
        {
            string istunnonLaite = Environment.MachineName.ToString();
            var viestit = db.Viestit.OrderByDescending(v => v.ViestiId).ToList();
            bool saapuvaLukematon = false;
            bool saapuva = false;
            foreach (var viesti in viestit)
            {
                try
                {
                    var sisaltoData = JsonConvert.DeserializeObject<SisaltoModel>(viesti.Sisalto);
                    if (istunnonLaite != sisaltoData.Laite && viesti.Luettu == 0)
                    {
                        saapuvaLukematon = true;
                        break;
                    }


                }
                catch (JsonReaderException ex)
                {
                    //Virheen käsittelyt tähän..
                    continue;
                }

            }
            bool testiparam = saapuvaLukematon ;
            bool anyFound = db.Viestit.Any(row => row.Luettu == 0);
 
            {
                return Json(new
                {   
                    Saapuva = saapuva,
                    VierasViesti = saapuvaLukematon,
                    AnyUnread = anyFound
                }, JsonRequestBehavior.AllowGet);
            }

        }

        /*
        //Toiminnallisuus viestien hakemiselle hakusanojen perusteella @Toni
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
        }*/

        //Toiminnallisuus viestien luku kuittaukselle @Jani
        // GET: LueViestit
        public ActionResult LueViestit()
        {
            var lukukuittaus = db.Viestit.Where(v => v.Luettu == 0).ToList();
            foreach (var viesti in lukukuittaus)
            {
                viesti.Luettu = 1;
            }
            db.SaveChanges();

            return RedirectToAction("Index");
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
            string laiteNimi = Environment.MachineName.ToString();
            if (string.IsNullOrEmpty(laiteNimi))
            {
                laiteNimi = "Unknown";
            }
            ViewBag.laiteNimi = laiteNimi;
            return View();
        }


        // POST: Viestit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ViestiId,Lahettaja,Aika")] Viestit viestit, string Message, string laiteNimi)
        {
            if (ModelState.IsValid)
            {
                var jsonObject = new
                {
                    Message = Message,
                    Laite = laiteNimi
                };

                //Muutetaan JSON muotoon ja osoitetaan kolumni Sisalto
                viestit.Sisalto = JsonConvert.SerializeObject(jsonObject);

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