using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecten2Groep7.Models.Domain
{
    public interface IProductRepository
    {
        IQueryable<Product> FindAll();
        Product FindById(int productId);
        void Add(Product product);
        void Delete(Product product);
        void SaveChanges();
    }
}
