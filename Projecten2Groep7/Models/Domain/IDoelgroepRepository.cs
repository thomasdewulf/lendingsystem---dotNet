using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecten2Groep7.Models.Domain
{
    public interface IDoelgroepRepository
    {
        IQueryable<Doelgroep> FindAll();

        Doelgroep FindById(int id);

        void SaveChanges();
        Doelgroep FindByName(string naam);
    }
}
