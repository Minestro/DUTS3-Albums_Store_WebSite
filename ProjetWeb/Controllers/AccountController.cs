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
        
        [Authorize]
        public ActionResult Index()
        {
            Abonné user = ViewModels.User.getUserByID(int.Parse(User.Identity.Name));
            if (user != null)
            {
                return View(user);
            } else {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }
        }

        [Authorize]
        public ActionResult Manage()
        {
            Abonné user = ViewModels.User.getUserByID(int.Parse(User.Identity.Name));
            if (user != null)
            {
                ManageViewModel manageModel = new ManageViewModel();
                manageModel.init(user);
                return View(manageModel);
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Manage(ManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.saveChanges();
                ViewBag.MessageValidationResult = "Les modifications ont bien été enregistrés";
            } else
            {
                ViewBag.MessageValidationResult = "";
            }
            Abonné user = ViewModels.User.getUserByID(int.Parse(User.Identity.Name));
            if (user != null)
            {
                model.init(user);
                return View(model);
            } else
            {
                return RedirectToAction("Login", "Account");
            }
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