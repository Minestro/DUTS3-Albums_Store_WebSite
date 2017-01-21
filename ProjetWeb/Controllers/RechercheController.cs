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