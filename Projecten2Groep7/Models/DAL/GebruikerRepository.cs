using Projecten2Groep7.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Projecten2Groep7.Models.DAL
{
    public class GebruikerRepository : IGebruikerRepository
    {
        private CatalogusContext context;
        private readonly IDbSet<ApplicationUser> gebruikers;

        public GebruikerRepository(CatalogusContext context)
        {
            this.context = context;
            this.gebruikers = context.Users;
        }
        public IQueryable<ApplicationUser> FindAll()
        {
            return gebruikers.Include(g => g.Verlanglijst).Include(g => g.Reservaties);
        }

        public ApplicationUser FindByUserName(string userName)
        {
            return gebruikers.Include(g=>g.Verlanglijst).Include(g=>g.Reservaties).FirstOrDefault(g => g.UserName == userName);
        }

        public ApplicationUser FindBy(string gebruikersNummer)
        {
            return gebruikers.Include(g => g.Verlanglijst).Include(g => g.Reservaties).FirstOrDefault(g=>g.GebruikersNummer == gebruikersNummer);
        }

        public ApplicationUser FindByEmail(string email)
        {
            return gebruikers.Include(g => g.Verlanglijst).Include(g=>g.Reservaties).First(g => g.Email == email);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
            
        }      
    }
}