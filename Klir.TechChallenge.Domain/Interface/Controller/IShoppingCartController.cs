using Klir.TechChallenge.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Domain.Interface.Controller
{
    public interface IShoppingCartController
    {
        IEnumerable<ShoppingCartResponse> Get(string id);
        Task<Guid> Post(ShoppingCartRequest shoppingCart);

        Task<Guid> Inactive(ShoppingCartRequest shoppingCart);

        Task<Guid> Update(ShoppingCartRequest shoppingCart);

        Task<Guid> CloseCart(ShoppingCartRequest shoppingCart);
    }
}
