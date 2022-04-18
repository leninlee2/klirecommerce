using Klir.TechChallenge.Domain.Interface.Service;
using Klir.TechChallenge.Domain.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Klir.TechChallenge.Domain.Interface.Repository;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Domain.Service
{
    public class ProductService : IProductService
    {
        private IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<Guid> Add(Product entry)
        {
           await productRepository.AddProduct(entry);
           return entry.Id;
        }

        public IQueryable<Product> findAll()
        {
            return productRepository.findAll();
        }

        public Product findProductById(Guid guid)
        {
            return productRepository.findProductById(guid);
        }

        public async Task<Guid> Update(Product entry)
        {
           await productRepository.UpdateProduct(entry);
           return entry.Id;
        }

    }

}
