﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfDues
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SapEntities : DbContext
    {
        public SapEntities()
            : base("name=SapEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<OITM> OITM { get; set; }
        public virtual DbSet<OITW> OITW { get; set; }
        public virtual DbSet<OWHS> OWHS { get; set; }
        public virtual DbSet<ITM1> ITM1 { get; set; }
    }
}
