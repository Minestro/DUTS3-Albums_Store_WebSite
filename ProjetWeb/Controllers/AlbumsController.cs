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
    public class AlbumsController : Controller
    {
        private Classique_WebEntities db = new Classique_WebEntities();

        public ActionResult Index()
        {
            var album = db.Album.Include(a => a.Editeur).Include(a => a.Genre);
            return View(album.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            AlbumViewModels model = new AlbumViewModels(album);
            return View(model);
        }
    }
}
