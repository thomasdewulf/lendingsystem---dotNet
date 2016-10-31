using Projecten2Groep7.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Projecten2Groep7.Models.DAL.Mapper
{
    public class BlokkeringDagMapper : EntityTypeConfiguration<BlokkeringDag>
    {
        public BlokkeringDagMapper()
        {
            ToTable("BlokkeringDag");
            HasKey(b => b.BlokkeringDagId);
        }
    }
}