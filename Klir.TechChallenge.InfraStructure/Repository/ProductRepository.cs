using Klir.TechChallenge.InfraStructure.ContextModel;
using Klir.TechChallenge.Domain.EntityModel;
using Klir.TechChallenge.InfraStructure.Repository.EntityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Klir.TechChallenge.Domain.Interface.Repository;

namespace Klir.TechChallenge.InfraStructure.Repository
{
    public class ProductRepository : EntityRepository<Product>, IProductRepository
    {
        private readonly KlirContext context;

        public ProductRepository(KlirContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Guid> AddProduct(Product entry)
        {
            await base.Add(entry);
            return entry.Id;
        }

        public List<Product> findAllProducts()
        {
            return base.findAll().ToList();
        }

        public Product findProductById(Guid guid)
        {
            var result = base.findAll().Where(w => w.Id == guid).ToList();
            if (result.Count() > 0)
                return result.First();
            else
                return null;
        }

        public async Task<Guid> UpdateProduct(Product entry)
        {
           await base.Update(entry);
           return entry.Id;
        }

    }

}
