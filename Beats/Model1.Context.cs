﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Beats1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BeatsEntities : DbContext
    {
        public BeatsEntities()
            : base("name=BeatsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<admin> admins { get; set; }
        public DbSet<canremove> canremoves { get; set; }
        public DbSet<channel> channels { get; set; }
        public DbSet<correlation> correlations { get; set; }
        public DbSet<genre> genres { get; set; }
        public DbSet<history> histories { get; set; }
        public DbSet<song> songs { get; set; }
        public DbSet<song_add> song_add { get; set; }
        public DbSet<songgenre> songgenres { get; set; }
        public DbSet<trailer> trailers { get; set; }
        public DbSet<upload> uploads { get; set; }
        public DbSet<user> users { get; set; }
        public DbSet<usercreatedchannel> usercreatedchannels { get; set; }
        public DbSet<userfollowschannel> userfollowschannels { get; set; }
    }
}
