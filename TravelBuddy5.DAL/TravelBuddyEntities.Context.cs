﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TravelBuddy5.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<POI> POI { get; set; }
        public virtual DbSet<Tour> Tour { get; set; }
        public virtual DbSet<TourPOI> TourPOI { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserPOI> UserPOI { get; set; }
        public virtual DbSet<UserTour> UserTour { get; set; }
    }
}
