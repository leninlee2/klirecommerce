using Klir.TechChallenge.Domain.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Domain.Interface.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Guid> AddProduct(Product entry);

        List<Product> findAllProducts();

        Task<Guid> UpdateProduct(Product entry);

        Product findProductById(Guid guid);
    }

}
