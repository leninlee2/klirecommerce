using Klir.TechChallenge.Domain.EntityModel;
using Klir.TechChallenge.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Domain.Interface.Service
{
    public interface IShoppingCartService : IService<ShoppingCart>
    {
        IQueryable<ShoppingCartResponse> findAllComplete();
        Task<Guid> Inactive(ShoppingCartRequest entry);

        Task<Guid> UpdateValue(ShoppingCartRequest entry);
    }

}
