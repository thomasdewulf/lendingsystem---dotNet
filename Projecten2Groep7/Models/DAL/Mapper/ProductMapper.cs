using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Models.DAL.Mapper
{
    public class ProductMapper : EntityTypeConfiguration<Product>
    {
        public ProductMapper()
        {
            ToTable("Product");
            HasKey(p => p.ProductId);
            HasMany(p => p.Doelgroepen).WithMany().Map(p => p.MapLeftKey("DoelgroepId").MapRightKey("ProductId"));
            HasMany(p => p.Leergebieden).WithMany().Map(p => p.MapLeftKey("LeergebiedId").MapRightKey("ProductId"));
            HasRequired(p => p.Firma).WithMany().Map(m => m.MapKey("FirmaId"));
            HasMany(p => p.ReservatieLijnen).WithRequired(p => p.Product).Map(p => p.MapKey("ProductId"));


        }
    }
}