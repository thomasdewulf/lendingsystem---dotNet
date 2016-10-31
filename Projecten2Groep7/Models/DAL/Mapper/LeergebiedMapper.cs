using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Models.DAL.Mapper
{
    public class LeergebiedMapper : EntityTypeConfiguration<Leergebied>
    {
        public LeergebiedMapper()
        {
            ToTable("Leergebied");
            HasKey(d => d.LeergebiedId);

        }
    }
}