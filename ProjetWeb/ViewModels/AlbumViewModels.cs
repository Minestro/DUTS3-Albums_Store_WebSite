using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetWeb.com.amazon.amazonaws;
using ProjetWeb.Models;

namespace ProjetWeb.ViewModels
{
    public class AlbumViewModels
    {
        public Album Album {get; set;}
        public string Description {get; set;}
        public string Prix { get; set; }
        public List<Enregistrement> Enregistrements { get; set; } 
         
        public AlbumViewModels (Album album)
        {
            Album = album;
            Description = "Aucune Description";
            Prix = "NAN";
            Classique_WebEntities db = new Classique_WebEntities();
            if (album!=null)
            {
                Enregistrements = db.Album.Where(c => c.Code_Album == album.Code_Album)
                    .SelectMany(c => db.Disque.Where(o => o.Code_Album == c.Code_Album))
                    .SelectMany(c => db.Composition_Disque.Where(o => o.Code_Disque == c.Code_Disque))
                    .SelectMany(c => db.Enregistrement.Where(o => o.Code_Morceau == c.Code_Morceau)).ToList();
            }

        }
    }
}