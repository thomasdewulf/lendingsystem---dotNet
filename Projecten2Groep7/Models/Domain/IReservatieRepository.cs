using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecten2Groep7.Models.Domain
{
    public interface IReservatieRepository
    {
        IQueryable<Reservatie> FindAll();
        Reservatie FindById(int reservatieId);
        void Add(Reservatie reservatie);
        void Delete(Reservatie reservatie);
        void SaveChanges();
        int BerekenAantalReservaties();
    }
}
