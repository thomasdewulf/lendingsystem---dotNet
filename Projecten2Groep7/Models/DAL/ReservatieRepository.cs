using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Projecten2Groep7.Models.DAL;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Models.DAL
{
    public class ReservatieRepository : IReservatieRepository
    {
        private CatalogusContext context;
        private DbSet<Reservatie> reservaties;
        public ReservatieRepository(CatalogusContext context)
        {
            this.context = context;
            this.reservaties = context.Reservaties;
        }
        public IQueryable<Reservatie> FindAll()
        {
            return reservaties;
        }

        public Reservatie FindById(int reservatieId)
        {
            return reservaties.Find(reservatieId);
        }

        public void Add(Reservatie reservatie)
        {
            reservaties.Add(reservatie);
        }

        public void Delete(Reservatie reservatie)
        {
            reservaties.Remove(reservatie);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public int BerekenAantalReservaties()
        {
            return reservaties.Count();
        }
    }
}