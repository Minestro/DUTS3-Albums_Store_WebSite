//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Disque
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Disque()
        {
            this.Composition_Disque = new HashSet<Composition_Disque>();
        }
    
        public int Code_Disque { get; set; }
        public int Code_Album { get; set; }
        public string Référence_Album { get; set; }
        public string Référence_Disque { get; set; }
    
        public virtual Album Album { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Composition_Disque> Composition_Disque { get; set; }
    }
}
