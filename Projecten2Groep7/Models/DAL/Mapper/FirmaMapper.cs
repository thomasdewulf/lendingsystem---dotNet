using Projecten2Groep7.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Projecten2Groep7.Models.DAL.Mapper
{
    public class FirmaMapper : EntityTypeConfiguration<Firma>
    {
        public FirmaMapper()
        {
            ToTable("Firma");
            HasKey(f => f.FirmaId);
        }
    }
}