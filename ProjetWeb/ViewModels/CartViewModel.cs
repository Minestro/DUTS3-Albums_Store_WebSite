using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetWeb.Models;

namespace ProjetWeb.ViewModels
{
    public class CartViewModel
    {
        public List<Enregistrement> Enregistrements {get; set;}
        public float PrixTotal { get; set; }
    }
}