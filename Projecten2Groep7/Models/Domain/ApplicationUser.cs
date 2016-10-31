using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Projecten2Groep7.Models.DAL;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public abstract class ApplicationUser:IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string GebruikersNummer { get; set; }
        public virtual string Naam { get; set; }
        public virtual string Voornaam { get; set; }
        public virtual Verlanglijst Verlanglijst { get; set; }
        public virtual ICollection<Reservatie> Reservaties { get; set; }
        public string Foto { get; set; }

        public abstract void VoegReservatieToe(Dictionary<Product, int> map, DateTime startDate, DateTime eindDate,Email[] email, bool[] dagen = null);
   
    }


}