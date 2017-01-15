using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetWeb.Models;

namespace ProjetWeb.ViewModels
{
    public class OeuvreViewModel
    {
        public Oeuvre Oeuvre { get; set; }
        public List<Musicien> Compositeurs { get; set; }
        public List<Album> Albums { get; set; }

        public OeuvreViewModel(Oeuvre oeuvre, Classique_WebEntities db)
        {
            Oeuvre = oeuvre;
            if (oeuvre != null)
            {
                Compositeurs = db.Oeuvre.Where(c => c.Code_Oeuvre == oeuvre.Code_Oeuvre)
                    .SelectMany(c => db.Composer.Where(o => o.Code_Oeuvre == c.Code_Oeuvre))
                    .SelectMany(c => db.Musicien.Where(o => o.Code_Musicien == c.Code_Musicien)).Distinct().ToList();

                Albums = db.Oeuvre.Where(c => c.Code_Oeuvre == oeuvre.Code_Oeuvre)
                    .SelectMany(c => db.Composition_Oeuvre.Where(o => o.Code_Oeuvre == c.Code_Oeuvre))
                    .SelectMany(c => db.Composition.Where(o => o.Code_Composition == c.Code_Composition))
                    .SelectMany(c => db.Enregistrement.Where(o => o.Code_Composition == c.Code_Composition))
                    .SelectMany(c => db.Composition_Disque.Where(o => o.Code_Morceau == c.Code_Morceau))
                    .SelectMany(c => db.Disque.Where(o => o.Code_Disque == c.Code_Disque))
                    .SelectMany(c => db.Album.Where(o => o.Code_Album == c.Code_Album)).Distinct().ToList();
            }
        }
    }
}