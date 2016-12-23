﻿using ProjetWeb.Models;
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

    public class ChangePasswordAttribute : ValidationAttribute
    {
        public ChangePasswordAttribute(int minLength, string oldPasswordPropertyName)
        {
            this.OldPasswordPropertyName = oldPasswordPropertyName;
            this.MinLength = minLength;
        }

        public string OldPasswordPropertyName { get; private set; }
        public int MinLength { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(OldPasswordPropertyName);
            string oldPassword = property.ToString();
            string actualPassword = User.getUserByID(int.Parse(HttpContext.Current.User.Identity.Name)).Password;
            string newPassword;
            if (value != null)
            {
               newPassword = value.ToString();
            } else
            {
                newPassword = "";
            }
            if (oldPassword.Length == 0)
            {
                return ValidationResult.Success;
            }
            if (actualPassword != Encode.EncodeMD5(oldPassword))
            {
                return new ValidationResult("L'ancien mot de passe n'est pas le bon", new string[] { OldPasswordPropertyName });
            }
            if (newPassword.Length < MinLength)
            {
                return new ValidationResult("Le nouveau mot de passe doit faire au minimum " + MinLength.ToString() + " caractères");
            }
            return ValidationResult.Success;
        }
    }

    public static class User
    {
        public static Abonné getUserByID(int id)
        {
            Classique_WebEntities db = new Classique_WebEntities();
            return db.Abonné.FirstOrDefault(u => u.Code_Abonné == id);
        }

        public static Abonné getUserByID(Classique_WebEntities db, int id)
        {
            return db.Abonné.FirstOrDefault(u => u.Code_Abonné == id);
        }
    }

    public class RegisterViewModel
    {
        public int Code_Abonné { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Nom_Abonné { get; set; }

        [Required]
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

    public class ManageViewModel
    {
        private int Id;

        public System.Web.Mvc.SelectList PaysList{ get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Nom_Abonné { get; set; }

        [Required]
        [Display(Name = "Prénom")]
        public string Prénom_Abonné { get; set; }

        [Display(Name = "Ancien Mot de Passe")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        [ChangePassword(6, "OldPassword")]
        public string NewPasswordRepeat { get; set; }

        public string Adresse { get; set; }

        public string Ville { get; set; }

        [Display(Name = "Code_Postal")]
        [DataType(DataType.PostalCode)]
        public string Code_Postal { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int? Code_Pays { get; set; }

        public void init(Abonné abonné)
        {
            Classique_WebEntities db = new Classique_WebEntities();
            PaysList = new System.Web.Mvc.SelectList(db.Pays, "Code_Pays", "Nom_Pays");
            if (abonné != null)
            {
                this.Id = abonné.Code_Abonné;
                this.Nom_Abonné = abonné.Nom_Abonné;
                this.Prénom_Abonné = abonné.Prénom_Abonné;
                this.Adresse = abonné.Adresse;
                this.Ville = abonné.Ville;
                this.Code_Pays = abonné.Code_Pays;
                this.Email = abonné.Email;
            }
        }

        public void saveChanges()
        {
            Classique_WebEntities db = new Classique_WebEntities();
            Abonné abonné = User.getUserByID(db, Id);
            if (abonné != null)
            {
                abonné.Nom_Abonné = this.Nom_Abonné;
                abonné.Prénom_Abonné = this.Prénom_Abonné;
                abonné.Adresse = this.Adresse;
                abonné.Ville = this.Ville;
                abonné.Code_Pays = this.Code_Pays;
                abonné.Email = this.Email;
                if (NewPassword.Length > 0)
                {
                    abonné.Password = Encode.EncodeMD5(NewPassword);
                }
            }
            db.SaveChanges();
        }
    }
}