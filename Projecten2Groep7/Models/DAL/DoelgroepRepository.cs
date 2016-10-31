using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Expressions;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Models.DAL
{


    public class DoelgroepRepository : IDoelgroepRepository
    {
        private CatalogusContext context;
        private readonly DbSet<Doelgroep> doelgroepen;

            public DoelgroepRepository(CatalogusContext context)
            {
                this.context = context;
                doelgroepen = context.Doelgroepen;
            }
        public IQueryable<Doelgroep> FindAll()
        {
            return doelgroepen.OrderBy(d => d.Naam);
        }

        public Doelgroep FindById(int id)
        {
            return doelgroepen.Find(id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public Doelgroep FindByName(string naam)
        {
            return doelgroepen.First(p => p.Naam == naam);
        }
    }
}