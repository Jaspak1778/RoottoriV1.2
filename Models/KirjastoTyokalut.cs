//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RoottoriV1._2.Models
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    
    public partial class KirjastoTyokalut
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KirjastoTyokalut()
        {
            this.MalliE25RiTyokalut = new HashSet<MalliE25RiTyokalut>();
            this.MalliE25RTyokalut = new HashSet<MalliE25RTyokalut>();
            this.MalliE6RTyokalut = new HashSet<MalliE6RTyokalut>();
        }
    
        public int TyokaluID { get; set; }
        public Nullable<int> TyokaluKategoriaID { get; set; }
        public int KoneID { get; set; }
        public int TyokaluNro { get; set; }
        //Lis�tty virheiden tarkistusta varten

        [Required(ErrorMessage = "Ty�kalun nimi on pakollinen")]
        public string TyokalunNimi { get; set; }

        public int Pituus { get; set; }
        public int Halkaisija { get; set; }
        public string Pala { get; set; }
        public string ImageLink { get; set; }
        public string Lisatieto1 { get; set; }
    
        public virtual Koneet Koneet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MalliE25RiTyokalut> MalliE25RiTyokalut { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MalliE25RTyokalut> MalliE25RTyokalut { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MalliE6RTyokalut> MalliE6RTyokalut { get; set; }
    }
}
