using Klir.TechChallenge.Domain.EntityModel;
using Klir.TechChallenge.Domain.Interface.Controller;
using Klir.TechChallenge.Domain.Interface.Repository;
using Klir.TechChallenge.Domain.Interface.Service;
using Klir.TechChallenge.Domain.Model;
using Klir.TechChallenge.Domain.Service;
using Klir.TechChallenge.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase, IShoppingCartController
    {

        private readonly IShoppingCartService shoppingCartService;
        private readonly IGroupShoopingCartRepository groupShoopingCartRepository;
        private readonly IGroupShoppingCartService groupShoppingCartService;

        public ShoppingCartController(
              IShoppingCartService shoppingCartService
            , IGroupShoopingCartRepository groupShoopingCartRepository
            , IGroupShoppingCartService groupShoppingCartService
            )
        {
            this.shoppingCartService = shoppingCartService;
            this.groupShoopingCartRepository = groupShoopingCartRepository;
            this.groupShoppingCartService = groupShoppingCartService;
        }

        [HttpGet("{id}")]
        public IEnumerable<ShoppingCartResponse> Get(string id)
        {
            var activeGroups = groupShoopingCartRepository.findAll().Where(w => w.IdClient == new Guid(id) && w.Status == StatusItem.Active
              && w.Close != StatusItem.Active
            ).ToList();
            if (activeGroups.Count() > 0)
            {
                Guid group = activeGroups.First().Id;
                return shoppingCartService.findAllComplete().Where(w => w.shoppingCart.IdGroupShoppingCart == group).ToList();
            }
            else
                return new List<ShoppingCartResponse>();
            //return shoppingCartService.findAllComplete();
        }

        [HttpPost]
        public Task<Guid> Post(ShoppingCartRequest shoppingCart)
        {
            ShoppingCart entry = new ShoppingCart() { Id= Guid.NewGuid(),AlterDate=DateTime.Now,CreateDate=DateTime.Now
            ,IdProduct = new Guid(shoppingCart.IdProduct),Quantity=shoppingCart.Quantity,Status= StatusItem.Active
            };

            var groups = groupShoopingCartRepository.findAll().Where(w => w.IdClient == new Guid(shoppingCart.IdClient) && w.Status==StatusItem.Active 
             && w.Close!=StatusItem.Active
            ).ToList();
            if(groups.Count() > 0)
            {
                entry.IdGroupShoppingCart = groups.First().Id;
            }
            else
            {
                GroupShoopingCart group = new GroupShoopingCart() { Id = Guid.NewGuid(), IdClient = new Guid(shoppingCart.IdClient), CreateDate = DateTime.Now, Status = StatusItem.Active };
                groupShoopingCartRepository.Add(group);
                entry.IdGroupShoppingCart = group.Id;
            }

            return shoppingCartService.Add(entry);
        }

        [HttpPost("inactive")]
        public Task<Guid> Inactive(ShoppingCartRequest shoppingCart)
        {
            return shoppingCartService.Inactive(shoppingCart);
        }

        [HttpPost("update")]
        public Task<Guid> Update(ShoppingCartRequest shoppingCart)
        {
            return shoppingCartService.UpdateValue(shoppingCart);
        }

        [HttpPost("closecart")]
        public async Task<Guid> CloseCart(ShoppingCartRequest shoppingCart)
        {
            var groups = groupShoopingCartRepository.findAll().Where(w => w.IdClient == new Guid(shoppingCart.IdClient) && w.Status == StatusItem.Active && w.Close != StatusItem.Active).ToList();
            if (groups.Count() > 0)
            {
                var item = groups.Select(s => new GroupShoopingCartRequest() { Id = s.Id.ToString() }).First();
                await groupShoppingCartService.Close(item);
                return new Guid( item.Id);
            }
            else
                return new Guid();
                
        }

    }

}
