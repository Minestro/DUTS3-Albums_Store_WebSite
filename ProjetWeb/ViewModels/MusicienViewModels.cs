using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetWeb.Models;

namespace ProjetWeb.ViewModels
{
    public class MusicienViewModels
    {
        public Musicien Musicien { get; set; }
        public List<Oeuvre> Oeuvres { get; set; }

        public MusicienViewModels(Musicien musicien)
        {
            Musicien = musicien;
            Classique_WebEntities db = new Classique_WebEntities();
            if (musicien != null)
            {
                Oeuvres = db.Musicien.Where(c => c.Code_Musicien == musicien.Code_Musicien)
                    .SelectMany(c => db.Composer.Where(o => o.Code_Musicien == c.Code_Musicien))
                    .SelectMany(c => db.Oeuvre.Where(o => o.Code_Oeuvre == c.Code_Oeuvre)).ToList();
            }

        }
    }
}