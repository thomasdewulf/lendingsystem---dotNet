using Projecten2Groep7.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Projecten2Groep7.Models.DAL.Mapper
{
    public class GebruikerMapper : EntityTypeConfiguration<ApplicationUser>
    {
        public GebruikerMapper()
        {
            ToTable("Gebruiker");
            HasKey(g => g.Id);
            HasRequired(g => g.Verlanglijst).WithRequiredDependent().Map(g => g.MapKey("VerlanglijstId"));
            HasMany(g => g.Reservaties).WithRequired(g=>g.ReservatieUser).Map(g => g.MapKey("ApplicationUserId"));

        }
    }
}