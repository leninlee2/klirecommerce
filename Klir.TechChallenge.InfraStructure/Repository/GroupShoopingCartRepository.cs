using Klir.TechChallenge.Domain.EntityModel;
using Klir.TechChallenge.Domain.Interface.Repository;
using Klir.TechChallenge.InfraStructure.ContextModel;
using Klir.TechChallenge.InfraStructure.Repository.EntityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Klir.TechChallenge.InfraStructure.Repository
{
    public class GroupShoopingCartRepository : EntityRepository<GroupShoopingCart>, IGroupShoopingCartRepository
    {
        private readonly KlirContext context;

        public GroupShoopingCartRepository(KlirContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Guid> AddGroupShoopingCart(GroupShoopingCart entry)
        {
            await base.Add(entry);
            return entry.Id;
        }

        public List<GroupShoopingCart> findAllGroupShoopingCart()
        {
            return base.findAll().ToList();
        }

        public async Task<Guid> UpdateGroupShoopingCart(GroupShoopingCart entry)
        {
            await base.Update(entry);
            return entry.Id;
        }

    }

}
