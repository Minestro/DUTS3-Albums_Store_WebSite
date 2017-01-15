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
    }
}
