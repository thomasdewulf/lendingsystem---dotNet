using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Expressions;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Models.DAL
{


    public class LeergebiedRepository : ILeergebiedRepository
    {
        private CatalogusContext context;
        private readonly DbSet<Leergebied> leergebieden;

            public LeergebiedRepository(CatalogusContext context)
            {
                this.context = context;
                leergebieden = context.Leergebieden;
            }
        public IQueryable<Leergebied> FindAll()
        {
            return leergebieden.OrderBy(d => d.Naam);
        }

        public Leergebied FindById(int id)
        {
            return leergebieden.Find(id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public Leergebied FindByName(string naam)
        {
            return leergebieden.First(p => p.Naam == naam);
        }
    }
}