using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Models.DAL.Mapper
{
    public class DoelgroepMapper : EntityTypeConfiguration<Doelgroep>
    {
        public DoelgroepMapper()
        {
            ToTable("Doelgroep");
            HasKey(d => d.DoelgroepId);
            
        }
    }
}