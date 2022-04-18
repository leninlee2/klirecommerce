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
    public class ShoppingCartRepository : EntityRepository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly KlirContext context;

        public ShoppingCartRepository(KlirContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Guid> AddShoopingCart(ShoppingCart entry)
        {
            await base.Add(entry);
            return entry.Id;
        }

        public List<ShoppingCart> findAllShoopingCart()
        {
            return base.findAll().ToList();
        }

        public async Task<Guid> UpdateShoopingCart(ShoppingCart entry)
        {
            await base.Update(entry);
            return entry.Id;
        }

    }

}
