using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Models.DAL.Mapper
{
    public class ReservatieMapper : EntityTypeConfiguration<Reservatie>
    {
        public ReservatieMapper()
        {
            ToTable("Reservatie");
            HasKey(r => r.ReservatieId);
            HasMany(r => r.ReservatieLijnen).WithRequired(r => r.Reservatie).Map(r => r.MapKey("ReservatieId"));

        }
    }
}