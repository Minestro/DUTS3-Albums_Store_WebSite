using ProjetWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ProjetWeb.ViewModels
{
    static class Encode
    {
        public static string EncodeMD5(string password)
        {
            string salt = "MotDePasse" + password + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(salt)));
        }
    }

    public static class User
    {
        public static Abonné getUserByID(int id)
        {
            Classique_WebEntities db = new Classique_WebEntities();
            return db.Abonné.FirstOrDefault(u => u.Code_Abonné == id);
        }
    }

    public class RegisterViewModel
    {
        public int Code_Abonné { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Nom_Abonné { get; set; }

        [Display(Name = "Prénom")]
        public string Prénom_Abonné { get; set; }

        [Required]
        [Display(Name = "Pseudo")]
        public string Login { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe ")]
        [Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string PasswordConfirmation { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public Abonné register()
        {
            string passwordEncoded = Encode.EncodeMD5(Password);
            Classique_WebEntities db = new Classique_WebEntities();
            Abonné abonné = new Abonné { Nom_Abonné = this.Nom_Abonné, Prénom_Abonné = this.Prénom_Abonné, Login = this.Login, Password = passwordEncoded, Email = this.Email, Credit = 0 };
            db.Abonné.Add(abonné);
            db.SaveChanges();
            return abonné;
        }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Pseudo ou Email")]
        public string LoginOuEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        public Abonné login()
        {
            string passwordEncoded = Encode.EncodeMD5(Password);
            Classique_WebEntities db = new Classique_WebEntities();
            Abonné user = db.Abonné.FirstOrDefault(u => u.Login == LoginOuEmail && u.Password == passwordEncoded);
            if (user == null)
            {
                return db.Abonné.FirstOrDefault(u => u.Email == LoginOuEmail && u.Password == passwordEncoded);
            }
            else
            {
                return user;
            }
        }
    }
}