using Klir.TechChallenge.Domain.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Domain.Interface.Repository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        Task<Guid> AddShoopingCart(ShoppingCart entry);
        List<ShoppingCart> findAllShoopingCart();

        Task<Guid> UpdateShoopingCart(ShoppingCart entry);
    }
}
