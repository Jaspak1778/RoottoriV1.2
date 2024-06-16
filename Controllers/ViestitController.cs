using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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

        public ActionResult Index(string searchTerm)  //Viesti on JSON muodossa

        {
            #region Istunnon tunnistus
            //haetaan viestit, sisätlö JSON muodossa, viestin sisältöön on korvamerkitty laite tai voidaan muuttaa IP osoitteksi myöhemmin, kumpi on parempi @Jani
            /*ViewBag.Host = Environment.MachineName.ToString();*/

            //Muutettu istunto IP pohjaiseksi
            string ip = Request.UserHostAddress;
            ViewBag.Host = ip;

            #endregion

            var viestit = db.Viestit.OrderByDescending(v => v.ViestiId).ToList();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                string SearchTerm = searchTerm.ToLower(); //Eliminoidaan Case sensitiivisyys
                viestit = viestit.FindAll(v => v.Lahettaja.ToLower().Contains(SearchTerm) || v.Sisalto.ToLower().Contains(SearchTerm));
            }

            foreach (var viesti in viestit)
            {
                try
                {
                    var sisaltoData = JsonConvert.DeserializeObject<SisaltoModel>(viesti.Sisalto); //Haetaan viestit ja lajitellaan tietueet luokan avulla
                    viesti.Message = sisaltoData.Message;
                    viesti.Laite = sisaltoData.Laite;
                }

                catch (JsonReaderException)   //Virheen korjaus jos viestin muoto ei ole JSON
                {
                    viesti.Message += viesti.Sisalto;
                }
            }

            return View(viestit);
        }
        // TODO: Edit ja Haku toiminnot
        //Taustatoiminto viesti liikenne notifikaatiot sekä tuleva ja lähtevän viestin logiikka @Jani
        public ActionResult ViestitService()
        {
            #region Istunnon tunnistus
            /*string istunnonLaite = Environment.MachineName.ToString();*/

            ////Muutettu istunto IP pohjaiseksi
            string istunnonLaite;
            istunnonLaite = Request.UserHostAddress;

            #endregion

            var viestit = db.Viestit.OrderByDescending(v => v.ViestiId).ToList();
            bool saapuvaLukematon = false;
            bool saapuva = false;
            foreach (var viesti in viestit)
            {
                try
                {
                    var sisaltoData = JsonConvert.DeserializeObject<SisaltoModel>(viesti.Sisalto);  //Kerätään tietoa viestiliikenteestä, tulevia.
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
            bool testiparam = saapuvaLukematon;
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

        //Toiminnallisuus viestien luku kuittaukselle @Jani
        // GET: LueViestit
        public ActionResult LueViestit(int? id)
        {
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
                viestit.Luettu = 1;
                db.SaveChanges();
                return RedirectToAction("Index");
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

            #region Istunnon tunnistus
            //*string laiteNimi = Environment.MachineName.ToString();  //Haetaan laitteen nimi.
            //Muutettu IP pohjaiseksi*/

            string laiteNimi = Request.UserHostAddress;

            if (string.IsNullOrEmpty(laiteNimi))
            {
                laiteNimi = "Unknown";
            }
            #endregion

            ViewBag.laiteNimi = laiteNimi; // Lähetetään laitenimi clientille
            ViewBag.Aika = DateTime.Now;
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
                viestit.Aika = DateTime.Now;
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
            Viestit viestit = db.Viestit.Find(id); //Haetaan viestit
            if (viestit == null)
            {
                return HttpNotFound();
            }
            try
            {
                var sisaltoData = JsonConvert.DeserializeObject<SisaltoModel>(viestit.Sisalto);  //Populoidaan kentät
                viestit.Message = sisaltoData.Message;
                viestit.Laite = sisaltoData.Laite;  //Piilotettu formissa
                viestit.Aika = DateTime.Now;        //Haetaan aikaleima
            }
            catch (JsonReaderException ex)

            {
                string error = ex.Message;

                var Viestit = db.Viestit.Find(id);

            }
            return View(viestit);
        }

        // POST: Viestit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ViestiId,Lahettaja,Aika")] Viestit viestit, string Message, string Laite)
        {
            if (ModelState.IsValid)
            {
                var jsonObject = new
                {
                    Message = Message,
                    Laite = Laite
                };

                //Muutetaan JSON muotoon ja osoitetaan kolumni Sisalto
                viestit.Sisalto = JsonConvert.SerializeObject(jsonObject);

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
            Viestit viestit = db.Viestit.Find(id); //Haetaan viestit
            if (viestit == null)
            {
                return HttpNotFound();
            }
            try
            {
                var sisaltoData = JsonConvert.DeserializeObject<SisaltoModel>(viestit.Sisalto);  //Populoidaan kentät
                viestit.Message = sisaltoData.Message;
                viestit.Laite = sisaltoData.Laite;  //Piilotettu formissa
                viestit.Aika = DateTime.Now;        //Haetaan aikaleima
            }
            catch (JsonReaderException ex)

            {
                string error = ex.Message;

                var Viestit = db.Viestit.Find(id);

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