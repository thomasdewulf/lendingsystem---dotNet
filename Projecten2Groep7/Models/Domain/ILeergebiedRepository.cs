using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecten2Groep7.Models.Domain
{
    public interface ILeergebiedRepository
    {
        IQueryable<Leergebied> FindAll();

        Leergebied FindById(int id);

        void SaveChanges();
       Leergebied FindByName(string naam);
    }
}
