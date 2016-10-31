using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Projecten2Groep7.Models.Domain;

namespace Projecten2Groep7.Models.DAL
{
    public class ProductRepository: IProductRepository
    {
        private CatalogusContext context;
        private readonly DbSet<Product> producten;

        public ProductRepository(CatalogusContext context)
        {
            this.context = context;
            producten = context.Producten;
        }
        public IQueryable<Product> FindAll()
        {
            return producten.OrderBy(p => p.Artikelnaam);
        }

        public Product FindById(int productId)
        {
            return producten.Find(productId);
        }

        public void Add(Product product)
        {
            producten.Add(product);
        }

        public void Delete(Product product)
        {
            producten.Remove(product);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
}
}