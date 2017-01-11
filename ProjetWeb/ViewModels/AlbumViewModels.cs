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
        public Album Album {get; set;}  //L'album en lui même
        public string Description {get; set;}   //Les infos complémentaires obtenus avec l'API Amazon
        public string Prix { get; set; }
        public List<Enregistrement> Enregistrements { get; set; }   //Les morceaux de l'album
        public List<Composition> Compositeurs { get; set; } //Les compositeurs de cet album
        public List<Album> Albums { get; set; } //Les albums du même artiste

        public AlbumViewModels (Album album)
        {
            Album = album;
            Description = "Aucune Description";
            Prix = "NAN";
            Classique_WebEntities db = new Classique_WebEntities();
            if (album != null)
            {
                Enregistrements = db.Album.Where(c => c.Code_Album == album.Code_Album)
                    .SelectMany(c => db.Disque.Where(o => o.Code_Album == c.Code_Album))
                    .SelectMany(c => db.Composition_Disque.Where(o => o.Code_Disque == c.Code_Disque))
                    .SelectMany(c => db.Enregistrement.Where(o => o.Code_Morceau == c.Code_Morceau)).ToList();

                Albums = db.Album.

                //TODO
                /*Compositeurs = db.Album.Where(m => m.Code_Album == album.Code_Album)
                    .SelectMany(m => db.Genre.Where(p => p.Code_Genre == m.Code_Genre))
                    .SelectMany(m => db.Musicien.Where(p => p.Code_Genre == m.Code_Genre)).ToList();
                    .SelectMany(m => db.Composer.Where(p => p.Code_Musicien == m.Code_Musicien)).ToList();*/
            }

        }
    }
}