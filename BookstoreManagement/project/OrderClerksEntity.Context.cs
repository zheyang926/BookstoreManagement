﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace project
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class projectEntities : DbContext
    {
        public projectEntities()
            : base("name=projectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<author> authors { get; set; }
        public virtual DbSet<book> books { get; set; }
        public virtual DbSet<bookOrder> bookOrders { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<customerOrder> customerOrders { get; set; }
        public virtual DbSet<invoice> invoices { get; set; }
        public virtual DbSet<publisher> publishers { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}
