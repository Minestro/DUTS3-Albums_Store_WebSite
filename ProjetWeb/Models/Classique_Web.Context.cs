﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjetWeb.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Classique_WebEntities : DbContext
    {
        public Classique_WebEntities()
            : base("name=Classique_WebEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Abonné> Abonné { get; set; }
        public virtual DbSet<Achat> Achat { get; set; }
        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<Composer> Composer { get; set; }
        public virtual DbSet<Composition> Composition { get; set; }
        public virtual DbSet<Composition_Disque> Composition_Disque { get; set; }
        public virtual DbSet<Composition_Oeuvre> Composition_Oeuvre { get; set; }
        public virtual DbSet<Direction> Direction { get; set; }
        public virtual DbSet<Disque> Disque { get; set; }
        public virtual DbSet<Editeur> Editeur { get; set; }
        public virtual DbSet<Enregistrement> Enregistrement { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Instrument> Instrument { get; set; }
        public virtual DbSet<Instrumentation> Instrumentation { get; set; }
        public virtual DbSet<Interpréter> Interpréter { get; set; }
        public virtual DbSet<Musicien> Musicien { get; set; }
        public virtual DbSet<Oeuvre> Oeuvre { get; set; }
        public virtual DbSet<Orchestres> Orchestres { get; set; }
        public virtual DbSet<Pays> Pays { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Type_Morceaux> Type_Morceaux { get; set; }
    }
}
