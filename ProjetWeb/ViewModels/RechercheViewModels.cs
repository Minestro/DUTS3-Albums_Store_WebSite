using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ProjetWeb.Models;

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

    public class RechercheCategorieViewModel
    {
        [Display(Name = "Genre")]
        public int? Code_Genre { get; set; }

        public System.Web.Mvc.SelectList Genres { get; set; }

        [Display(Name = "Instrument")]
        public int? Code_Instrument { get; set; }

        public System.Web.Mvc.SelectList Instruments { get; set; }

        public void init(Classique_WebEntities db)
        {
            Genres = new System.Web.Mvc.SelectList(db.Genre, "Code_Genre", "Libellé_Abrégé", Code_Genre);
            Instruments = new System.Web.Mvc.SelectList(db.Instrument, "Code_Instrument", "Nom_Instrument", Code_Genre);
        }
    }
}