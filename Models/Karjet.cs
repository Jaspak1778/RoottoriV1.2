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
    using System;
    using System.Collections.Generic;
    
    public partial class Karjet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Karjet()
        {
            this.MalliE6Rasetus = new HashSet<MalliE6Rasetus>();
            this.MalliE25Rasetus = new HashSet<MalliE25Rasetus>();
            this.MalliE25Rasetus1 = new HashSet<MalliE25Rasetus>();
            this.MalliE25Riasetus = new HashSet<MalliE25Riasetus>();
            this.Roottorit = new HashSet<Roottorit>();
        }
    
        public int KarkiID { get; set; }
        public string KarkiMalli { get; set; }
        public string ImageLink { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MalliE6Rasetus> MalliE6Rasetus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MalliE25Rasetus> MalliE25Rasetus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MalliE25Rasetus> MalliE25Rasetus1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MalliE25Riasetus> MalliE25Riasetus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Roottorit> Roottorit { get; set; }
    }
}
