using Klir.TechChallenge.Domain.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Domain.Interface.Repository
{
    public interface IGroupShoopingCartRepository : IRepository<GroupShoopingCart>
    {
        Task<Guid> AddGroupShoopingCart(GroupShoopingCart entry);
        List<GroupShoopingCart> findAllGroupShoopingCart();

        Task<Guid> UpdateGroupShoopingCart(GroupShoopingCart entry);
    }

}
