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
         
        public AlbumViewModels (Album album)
        {
            Album = album;
            if (Album.ASIN != null)
            {
                AWSECommerceService service = new AWSECommerceService();

                ItemLookup search = new ItemLookup();
                ItemLookupRequest request = new ItemLookupRequest();
                search.AssociateTag = "pro0f5-21";
                search.AWSAccessKeyId = "AKIAIU6KMB72HSTD6FKA";

                request.IdType = ItemLookupRequestIdType.ASIN;
                request.ItemId = new string[] { Album.ASIN };
                request.ResponseGroup = new string[] { "ItemAttributes" };

                search.Request = new ItemLookupRequest[] { request };

                ItemLookupResponse response = service.ItemLookup(search);

                if (response != null)
                {
                    //Attribuer la valeur a Description et Prix
                }
            } else {
                Description = "Aucune description";
                Prix = "NAN";
            }
        }
    }
}