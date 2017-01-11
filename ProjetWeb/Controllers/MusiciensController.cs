using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetWeb.Models;
using ProjetWeb.ViewModels;

namespace ProjetWeb.Controllers
{
    public class MusiciensController : Controller
    {
        private Classique_WebEntities db = new Classique_WebEntities();

        // GET: Musiciens
        public ActionResult Index()
        {
            var musicien = db.Musicien.Include(m => m.Genre).Include(m => m.Instrument).Include(m => m.Pays);
            return View(musicien.ToList());
        }

        // GET: Musiciens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musicien musicien = db.Musicien.Find(id);
            if (musicien == null)
            {
                return HttpNotFound();
            }
            MusicienViewModels model = new MusicienViewModels(musicien);
            return View(model);
        }

        // GET: Musiciens/Create
        public ActionResult Create()
        {
            ViewBag.Code_Genre = new SelectList(db.Genre, "Code_Genre", "Libellé_Abrégé");
            ViewBag.Code_Instrument = new SelectList(db.Instrument, "Code_Instrument", "Nom_Instrument");
            ViewBag.Code_Pays = new SelectList(db.Pays, "Code_Pays", "Nom_Pays");
            return View();
        }

        // POST: Musiciens/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code_Musicien,Nom_Musicien,Prénom_Musicien,Année_Naissance,Année_Mort,Code_Pays,Code_Genre,Code_Instrument,Photo")] Musicien musicien)
        {
            if (ModelState.IsValid)
            {
                db.Musicien.Add(musicien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Code_Genre = new SelectList(db.Genre, "Code_Genre", "Libellé_Abrégé", musicien.Code_Genre);
            ViewBag.Code_Instrument = new SelectList(db.Instrument, "Code_Instrument", "Nom_Instrument", musicien.Code_Instrument);
            ViewBag.Code_Pays = new SelectList(db.Pays, "Code_Pays", "Nom_Pays", musicien.Code_Pays);
            return View(musicien);
        }

        // GET: Musiciens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musicien musicien = db.Musicien.Find(id);
            if (musicien == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_Genre = new SelectList(db.Genre, "Code_Genre", "Libellé_Abrégé", musicien.Code_Genre);
            ViewBag.Code_Instrument = new SelectList(db.Instrument, "Code_Instrument", "Nom_Instrument", musicien.Code_Instrument);
            ViewBag.Code_Pays = new SelectList(db.Pays, "Code_Pays", "Nom_Pays", musicien.Code_Pays);
            return View(musicien);
        }

        // POST: Musiciens/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_Musicien,Nom_Musicien,Prénom_Musicien,Année_Naissance,Année_Mort,Code_Pays,Code_Genre,Code_Instrument,Photo")] Musicien musicien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musicien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Code_Genre = new SelectList(db.Genre, "Code_Genre", "Libellé_Abrégé", musicien.Code_Genre);
            ViewBag.Code_Instrument = new SelectList(db.Instrument, "Code_Instrument", "Nom_Instrument", musicien.Code_Instrument);
            ViewBag.Code_Pays = new SelectList(db.Pays, "Code_Pays", "Nom_Pays", musicien.Code_Pays);
            return View(musicien);
        }

        // GET: Musiciens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musicien musicien = db.Musicien.Find(id);
            if (musicien == null)
            {
                return HttpNotFound();
            }
            return View(musicien);
        }

        // POST: Musiciens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Musicien musicien = db.Musicien.Find(id);
            db.Musicien.Remove(musicien);
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
