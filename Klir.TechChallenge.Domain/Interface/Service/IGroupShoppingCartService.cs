using Klir.TechChallenge.Domain.EntityModel;
using Klir.TechChallenge.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Domain.Interface.Service
{
    public interface IGroupShoppingCartService : IService<GroupShoopingCart>
    {
        Task<Guid> Add(GroupShoopingCartRequest entry);

        Task<Guid> Inactive(GroupShoopingCartRequest entry);

        Task<Guid> Close(GroupShoopingCartRequest entry);
    }
}
