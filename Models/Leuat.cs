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

    public partial class Leuat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Leuat()
        {
            this.Roottorit = new HashSet<Roottorit>();
        }

        public int LeukaID { get; set; }
        public string Leuat1 { get; set; }

        //Lis�tty virheentarkistusta varten @Toni
        [Required(ErrorMessage = "LeukaAsetus on pakollinen")]
        public string LeukaAsetus { get; set; }

        //Lis�tty virheentarkistusta varten @Toni
        [Required(ErrorMessage = "Leukapaine on pakollinen")]
        public string Leukapaine { get; set; }

        public string ImageLink { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Roottorit> Roottorit { get; set; }
    }
}
