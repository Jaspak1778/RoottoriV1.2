﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RoottoriDBEntities2 : DbContext
    {
        public RoottoriDBEntities2()
            : base("name=RoottoriDBEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Karjet> Karjet { get; set; }
        public virtual DbSet<KirjastoTyokalut> KirjastoTyokalut { get; set; }
        public virtual DbSet<Koneet> Koneet { get; set; }
        public virtual DbSet<Leuat> Leuat { get; set; }
        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<Magneetit> Magneetit { get; set; }
        public virtual DbSet<MalliE25RiTyokalut> MalliE25RiTyokalut { get; set; }
        public virtual DbSet<MalliE25RTyokalut> MalliE25RTyokalut { get; set; }
        public virtual DbSet<MalliE6RTyokalut> MalliE6RTyokalut { get; set; }
        public virtual DbSet<Paletit> Paletit { get; set; }
        public virtual DbSet<Piirustukset> Piirustukset { get; set; }
        public virtual DbSet<Roottorit> Roottorit { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Viestit> Viestit { get; set; }
    }
}
