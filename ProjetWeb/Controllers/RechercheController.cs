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

        public ActionResult RechercheAlbum(string recherche)
        {
            if (recherche == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Album> albums = db.Album.Where(m => m.Titre_Album.Contains(recherche)).ToList();
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
    }
}