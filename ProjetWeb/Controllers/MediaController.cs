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
    public class MediaController : Controller
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

        public ActionResult ExtraitMusique(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enregistrement enregistrement = db.Enregistrement.Find(id);
            if (enregistrement == null)
            {
                return HttpNotFound();
            }
            return base.File(enregistrement.Extrait, "audio/mpeg");
        }

        public ActionResult PhotoMusicien(int? id)
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
            return base.File(musicien.Photo, "image/jpg");
        }
    }
}