using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetWeb.Models;
using ProjetWeb.ViewModels;

namespace ProjetWeb.Controllers
{
    public class HomeController : Controller
    {
        Classique_WebEntities db = new Classique_WebEntities();
        // GET: Home
        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel(db);
            return View(model);
        }

        public ActionResult APropos()
        {
            return View();
        }
    }
}