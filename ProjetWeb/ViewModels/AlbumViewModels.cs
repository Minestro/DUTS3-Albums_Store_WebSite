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
        public List<Musicien> Compositeurs { get; set; } //Les compositeurs de cet album
        public List<Musicien> Artistes { get; set; } //Liste des artistes ayant participé à cet album
        public List<Album> Albums { get; set; } //Les albums du même genre

        public AlbumViewModels (Album album, Classique_WebEntities db)
        {
            Album = album;
            Description = "Aucune Description";
            Prix = "NAN";
            if (album != null)
            {
                Enregistrements = db.Album.Where(c => c.Code_Album == album.Code_Album)
                    .SelectMany(c => db.Disque.Where(o => o.Code_Album == c.Code_Album))
                    .SelectMany(c => db.Composition_Disque.Where(o => o.Code_Disque == c.Code_Disque))
                    .SelectMany(c => db.Enregistrement.Where(o => o.Code_Morceau == c.Code_Morceau)).ToList();

                Albums = db.Album.Where(c => c.Code_Genre == album.Code_Genre).ToList();
                if (Albums != null)
                {
                    Random rng = new Random();
                    int n = Albums.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        Album value = Albums[k];
                        Albums[k] = Albums[n];
                        Albums[n] = value;
                    }
                }

                Artistes = db.Album.Where(c => c.Code_Album == album.Code_Album)
                    .SelectMany(c => db.Disque.Where(o => o.Code_Album == c.Code_Album))
                    .SelectMany(c => db.Composition_Disque.Where(o => o.Code_Disque == c.Code_Disque))
                    .SelectMany(c => db.Enregistrement.Where(o => o.Code_Morceau == c.Code_Morceau))
                    .SelectMany(c => db.Interpréter.Where(o => o.Code_Morceau == c.Code_Morceau))
                    .SelectMany(c => db.Musicien.Where(o => o.Code_Musicien == c.Code_Musicien)).Distinct().ToList();

                Compositeurs = db.Album.Where(c => c.Code_Album == album.Code_Album)
                    .SelectMany(c => db.Disque.Where(o => o.Code_Album == c.Code_Album))
                    .SelectMany(c => db.Composition_Disque.Where(o => o.Code_Disque == c.Code_Disque))
                    .SelectMany(c => db.Enregistrement.Where(o => o.Code_Morceau == c.Code_Morceau))
                    .SelectMany(c => db.Composition.Where(o => o.Code_Composition == c.Code_Composition))
                    .SelectMany(c => db.Composition_Oeuvre.Where(o => o.Code_Composition == c.Code_Composition))
                    .SelectMany(c => db.Oeuvre.Where(o => o.Code_Oeuvre == c.Code_Oeuvre))
                    .SelectMany(c => db.Composer.Where(o => o.Code_Oeuvre == c.Code_Oeuvre))
                    .SelectMany(c => db.Musicien.Where(o => o.Code_Musicien == c.Code_Musicien)).Distinct().ToList();
            }

        }
    }
}