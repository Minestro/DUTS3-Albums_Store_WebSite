using ProjetWeb.Models;
using ProjetWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            Abonné user = ViewModels.User.getUserByID(int.Parse(User.Identity.Name), db);
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
            Abonné user = ViewModels.User.getUserByID(int.Parse(User.Identity.Name), db);
            if (user != null)
            {
                ManageViewModel manageModel = new ManageViewModel();
                manageModel.init(user, db);
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
                model.saveChanges(db);
                ViewBag.MessageValidationResult = "Les modifications ont bien été enregistrés";
            } else
            {
                ViewBag.MessageValidationResult = "";
            }
            Abonné user = ViewModels.User.getUserByID(int.Parse(User.Identity.Name), db);
            if (user != null)
            {
                model.init(user, db);
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
                Abonné userLoged = user.login(db); 
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
                Abonné userRegistered = user.register(db);
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

        [Authorize]
        public ActionResult AddFunds()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddFunds(int quantity)
        {
            Abonné user = ViewModels.User.getUserByID(int.Parse(User.Identity.Name), db);
            if (user != null)
            {
                user.Credit += quantity;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Les fonds ont bien été ajoutés";
                return View(user);
            } else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }
        }

        [Authorize]
        public ActionResult AddToCart(int id)
        {
            TempData["SuccessMessage"] = "";
            TempData["ErrorMessage"] = "";
            Enregistrement enregistrement = db.Enregistrement.Find(id);
            if (enregistrement == null)
            {
                TempData["ErrorMessage"] = "Erreur lors de l'ajout de l'article à votre panier.";
            } else
            {
                if (Session["Cart"] == null || Session["Cart"].GetType() != typeof(List<int>))
                {
                    Session["Cart"] = new List<int>();
                }
                if (((List<int>)Session["Cart"]).Contains(enregistrement.Code_Morceau) == false)
                {
                    ((List<int>)Session["Cart"]).Add(enregistrement.Code_Morceau);
                    TempData["SuccessMessage"] = "L'article a bien été ajouté à votre panier.";
                }
            }
            return RedirectToAction("Cart", "Account");
        }

        [Authorize]
        public ActionResult RemovefromCart(int id)
        {
            TempData["SuccessMessage"] = "";
            TempData["ErrorMessage"] = "";
            if (Session["Cart"] != null && Session["Cart"].GetType() == typeof(List<int>))
            {
                ((List<int>)Session["Cart"]).Remove(id);
                TempData["SuccessMessage"] = "L'article a été supprimé de votre panier.";
            }
            return RedirectToAction("Cart", "Account");
        }

        [Authorize]
        public ActionResult Cart()
        {
            CartViewModel model = new CartViewModel();
            model.Enregistrements = new List<Enregistrement>();
            float totalPrice = 0.00F;
            if (Session["Cart"] == null || Session["Cart"].GetType() != typeof(List<int>))
            {
                Session["Cart"] = new List<int>();
            }
            foreach (var codeEnregistrement in ((List<int>)Session["Cart"]))
            {
                Enregistrement enregistrement = db.Enregistrement.Find(codeEnregistrement);
                if (enregistrement != null)
                {
                    model.Enregistrements.Add(enregistrement);
                    totalPrice += (float)enregistrement.Prix;
                }
            }
            model.PrixTotal = totalPrice;
            return View(model);
        }
    }
}