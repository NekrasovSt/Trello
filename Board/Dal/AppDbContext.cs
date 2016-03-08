using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Board.Dal
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Models.Board> Boards { get; set; }
        public DbSet<Models.Card> Cards { get; set; }
        public DbSet<Models.List> Lists { get; set; }
        public DbSet<Models.Comment> Comments { get; set; }
    }
}