using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Models.DAL.Mapper
{
    public class VerlanglijstMapper : EntityTypeConfiguration<Verlanglijst>
    {
        public VerlanglijstMapper()
        {
            ToTable("Verlanglijst");

          
            HasKey(v => v.VerlanglijstId);
            HasMany(v => v.Producten).WithMany().Map(p => p.MapLeftKey("VerlanglijstId").MapRightKey("ProductId"));
        }
    }
}