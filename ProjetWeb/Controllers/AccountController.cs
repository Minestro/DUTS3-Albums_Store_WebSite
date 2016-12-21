using ProjetWeb.Models;
using ProjetWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjetWeb.Controllers
{
    public class AccountController : Controller
    {
        private Classique_WebEntities db = new Classique_WebEntities();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Abonné userLoged = user.login(); 
                if (userLoged == null)
                {
                    ModelState.AddModelError("LoginOuEmail", "Login et/ou mot de passe incorrect(s)");
                } else
                {
                    FormsAuthentication.SetAuthCookie(userLoged.Code_Abonné.ToString(), false);
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(user);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                Abonné userRegistered = user.register();
                FormsAuthentication.SetAuthCookie(userRegistered.Code_Abonné.ToString(), false);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}