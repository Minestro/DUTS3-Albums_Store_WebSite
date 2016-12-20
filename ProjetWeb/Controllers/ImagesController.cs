using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ProjetWeb.Models;
using System.Net;

namespace ProjetWeb.Controllers
{
    public class ImagesController : Controller
    {
        private Classique_WebEntities db = new Classique_WebEntities();

        public ActionResult Index()
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult PochetteAlbum(int? id)
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
            return base.File(album.Pochette, "image/jpg");
        }
    }
}