using Klir.TechChallenge.Domain.EntityModel;
using Klir.TechChallenge.Domain.Interface.Repository;
using Klir.TechChallenge.Domain.Interface.Service;
using Klir.TechChallenge.Domain.Model;
using Klir.TechChallenge.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Domain.Service
{
    public class GroupShoppingCartService : IGroupShoppingCartService
    {
        private IGroupShoopingCartRepository groupShoopingCartRepository;

        public GroupShoppingCartService(IGroupShoopingCartRepository groupShoopingCartRepository)
        {
            this.groupShoopingCartRepository = groupShoopingCartRepository;
        }

        public async Task<Guid> Add(GroupShoopingCart entry)
        {
            await groupShoopingCartRepository.Add(entry);
            return entry.Id;
        }

        public Task<Guid> Add(GroupShoopingCartRequest entry)
        {
            GroupShoopingCart group = new GroupShoopingCart()
            {
                Id=Guid.NewGuid(),
                IdClient=new Guid(entry.IdClient),
                CreateDate=DateTime.Now,
                Status=StatusItem.Active
            };
            return Add(group);
        }

        public async Task<Guid> Close(GroupShoopingCartRequest entry)
        {
            Guid guid = new Guid(entry.Id);
            var item = groupShoopingCartRepository.findAllGroupShoopingCart().Where(w => w.Id == guid).FirstOrDefault();
            item.Close = StatusItem.Active;
            item.AlterDate = DateTime.Now;
            await Update(item);
            return item.Id;
        }

        public IQueryable<GroupShoopingCart> findAll()
        {
            return groupShoopingCartRepository.findAll();
        }

        public async Task<Guid> Inactive(GroupShoopingCartRequest entry)
        {
            Guid guid = new Guid(entry.Id);
            var item = groupShoopingCartRepository.findAllGroupShoopingCart().Where(w => w.Id == guid).FirstOrDefault();
            item.Status = StatusItem.Inactive;
            item.AlterDate = DateTime.Now;
            await Update(item);
            return item.Id;
        }

        public async Task<Guid> Update(GroupShoopingCart entry)
        {
            await groupShoopingCartRepository.UpdateGroupShoopingCart(entry);
            return entry.Id;
        }

    }

}
