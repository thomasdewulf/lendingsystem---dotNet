using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Models.DAL
{
    public class CatalogusContext : IdentityDbContext<ApplicationUser>
    {
        public CatalogusContext() : base("DidactischeLeermiddelen") {}
        public DbSet<Product> Producten { get; set; }
        public DbSet<Leergebied> Leergebieden { get; set; }
        public DbSet<Doelgroep> Doelgroepen { get; set; }
        public DbSet<Reservatie> Reservaties { get; set; }
        public DbSet<Email> Emails { get; set;}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public static CatalogusContext Create()
        {
            return DependencyResolver.Current.GetService<CatalogusContext>();
        }
     
    }
}