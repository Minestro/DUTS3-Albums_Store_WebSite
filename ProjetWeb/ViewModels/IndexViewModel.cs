using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetWeb.Models;

namespace ProjetWeb.ViewModels
{
    public class IndexViewModel
    {
        public List<Album> AlbumsALaffiche { get; set; }
        
        public IndexViewModel(Classique_WebEntities db)
        {
            AlbumsALaffiche = new List<Album>();

            var meilleursAlbums =
            db.Achat
            .Join(db.Enregistrement, c => c.Code_Enregistrement, o => o.Code_Morceau, (c, o) => new { Code_Morceau = o.Code_Morceau})
            .Join(db.Composition_Disque, c => c.Code_Morceau, o => o.Code_Morceau, (c, o) => new { Code_Disque = o.Code_Disque})
            .Join(db.Disque, c => c.Code_Disque, o => o.Code_Disque, (c, o) => new { Code_Album = o.Code_Album })
            .Join(db.Album, c => c.Code_Album, o => o.Code_Album, (c, o) => new {Album = o })
            .GroupBy(c => c.Album)
            .Select(s => new
            {
                Album = s.Key,
                Nb = s.Count()
            })
            .OrderByDescending(o => o.Nb).ToList();

            int i = 0;
            Random rnd = new Random();
            foreach (var album in meilleursAlbums)
            {
                AlbumsALaffiche.Add(album.Album);
                i++;
                if (i>4)
                {
                    break;
                }
            }
            while (i < 5)
            {
                AlbumsALaffiche.Add(db.Album.Find(rnd.Next(0, db.Album.Count())));
                i++;
            }
        }
    }
}