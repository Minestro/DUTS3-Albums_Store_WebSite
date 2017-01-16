using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetWeb.ViewModels
{
    public class RechercheViewModels
    {
        [Required]
        [Display(Name = "Vous recherchez")]
        public string TypeRecherche { get; set; }

        public System.Web.Mvc.SelectList TypesRecherche { get; set; }

        public string Recherche { get; set; }

        public RechercheViewModels()
        {
            List<string> liste = new List<string>();
            liste.Add("Une oeuvre");
            liste.Add("Un album");
            TypesRecherche = new System.Web.Mvc.SelectList(liste, "Une oeuvre");
        }
    }
}