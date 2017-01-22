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
    public class RechercheController : Controller
    {
        private Classique_WebEntities db = new Classique_WebEntities();

        public ActionResult Index()
        {
            RechercheViewModels model = new RechercheViewModels();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(RechercheViewModels model)
        {
            if (ModelState.IsValid)
            {
                if (model.TypeRecherche == "Un album")
                {
                    return RedirectToAction("RechercheAlbum", "Recherche", new { id = model.Recherche });
                } else if (model.TypeRecherche == "Une oeuvre")
                {
                    return RedirectToAction("RechercheOeuvre", "Recherche", new { id = model.Recherche });
                }
            }
            return View(model);
        }

        public ActionResult RechercheCategorie()
        {
            RechercheCategorieViewModel model = new RechercheCategorieViewModel();
            model.init(db);
            return View(model);
        }

        [HttpPost]
        public ActionResult RechercheCategorie(RechercheCategorieViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Code_Genre != null)
                {
                    List<KeyValuePair<Album, string>> result = new List<KeyValuePair<Album, string>>();
                    List<Album> tempList = db.Album.Where(c => c.Code_Genre == model.Code_Genre).ToList();
                    foreach(var album in tempList)
                    {
                        result.Add(new KeyValuePair<Album, string>(album, "NAN"));
                    }
                    return View("RechercheAlbum", result);
                } else if (model.Code_Instrument != null)
                {
                    List <Oeuvre> result = db.Instrument.Where(c => c.Code_Instrument == model.Code_Instrument)
                        .SelectMany(c => db.Instrumentation.Where(d => d.Code_Instrument == c.Code_Instrument))
                        .SelectMany(c => db.Oeuvre.Where(d => d.Code_Oeuvre == c.Code_Oeuvre)).ToList();
                    return View("RechercheOeuvre", result);
                } else
                {
                    ModelState.AddModelError("Code_Genre", "Veuillez spécifier au moins une recherche");
                    model.init(db);
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult RechercheAlbum(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Album> albums = db.Album.Where(m => m.Titre_Album.Contains(id)).ToList();
            List<KeyValuePair<Album, string>> albumsPrixé = new List<KeyValuePair<Album, string>>();
            foreach (var album in albums)
            {
                albumsPrixé.Add(new KeyValuePair<Album, string>(album, "NAN"));
            }
            if (albums == null)
            {
                return HttpNotFound();
            }
            return View(albumsPrixé);
        }

        public ActionResult RechercheOeuvre(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Oeuvre> model = db.Oeuvre.Where(m => m.Titre_Oeuvre.Contains(id)).ToList();
            return View(model);
        }
    }
}