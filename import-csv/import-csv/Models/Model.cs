using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace import_csv.Models
{
    public class Model : DbContext
    {
        public Model() : base("connectionMotoristas")
        {

        }

        public DbSet<Motorista> Motoristas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Motorista>().ToTable("Motoristas");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}