using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Models.DAL.Mapper
{
    public class ReservatieLijnMapper : EntityTypeConfiguration<ReservatieLijn>
    {
        public ReservatieLijnMapper()
        {
            ToTable("ReservatieLijn");
            HasKey(rl => rl.ReservatieLijnId);
            HasMany(rl => rl.Dagen).WithOptional().Map(m => m.MapKey("ReservatielijnId"));

        }
    }
}