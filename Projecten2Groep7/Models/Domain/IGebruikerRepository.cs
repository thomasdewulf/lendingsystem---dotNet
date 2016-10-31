using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projecten2Groep7.Models.Domain
{
    public interface IGebruikerRepository
    {
        ApplicationUser FindBy(string gebruikersNummer);
        ApplicationUser FindByEmail(string email);
        IQueryable<ApplicationUser> FindAll();
        ApplicationUser FindByUserName(string userName);
        void SaveChanges();

    }
}