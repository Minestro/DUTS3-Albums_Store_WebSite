using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetWeb.Models;
using ProjetWeb.ViewModels;
using System.Net;

namespace ProjetWeb.Controllers
{
    public class OeuvresController : Controller
    {
        Classique_WebEntities db = new Classique_WebEntities();
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oeuvre oeuvre = db.Oeuvre.Find(id);
            if (oeuvre == null)
            {
                return HttpNotFound();
            }
            OeuvreViewModel model = new OeuvreViewModel(oeuvre, db);
            return View(model);
        }
    }
}