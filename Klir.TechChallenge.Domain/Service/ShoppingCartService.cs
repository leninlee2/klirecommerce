using Klir.TechChallenge.Domain.Interface.Service;
using Klir.TechChallenge.Domain.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Klir.TechChallenge.Domain.Interface.Repository;
using Klir.TechChallenge.Domain.Model;
using Klir.TechChallenge.Domain.ValueObjects;

namespace Klir.TechChallenge.Domain.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        private IShoppingCartRepository shoppingCartRepository;
        private IProductRepository productRepository;
        private IPromotionRepository promotionRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository
            , IProductRepository productRepository
            , IPromotionRepository promotionRepository
            )
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
            this.promotionRepository = promotionRepository;
        }

        public async Task<Guid> Add(ShoppingCart entry)
        {
            var product = productRepository.findProductById(entry.IdProduct);
            entry.Price = product.Price;
            entry.Total = entry.Price * entry.Quantity;
            entry.IdPromotion = product.IdPromotion;
            await shoppingCartRepository.AddShoopingCart(entry);
            return entry.Id;
        }

        public IQueryable<ShoppingCart> findAll()
        {
            return shoppingCartRepository.findAll();
        }

        public IQueryable<ShoppingCartResponse> findAllComplete()
        {
            int qtdePromotions = 0;
            int qtdeProducts = 0;
            //return only active items:
            var shoppings = findAll().Where(w => w.Status==StatusItem.Active).ToList().Select(s => new ShoppingCartResponse()
            {
                shoppingCart=s
            }).ToList();
            for(var i = 0;i < shoppings.Count(); i++)
            {
                qtdeProducts = productRepository.findAllProducts().Where(w => w.Id == shoppings[i].shoppingCart.IdProduct).Count();
                if (shoppings[i].shoppingCart.IdProduct != null && qtdeProducts > 0)
                    shoppings[i].product = productRepository.findProductById(shoppings[i].shoppingCart.IdProduct);

                qtdePromotions = promotionRepository.findAllPromotion().Where(w => w.Id == shoppings[i].shoppingCart.IdPromotion).Count();
                if (shoppings[i].shoppingCart.IdPromotion != null && qtdePromotions > 0)
                    shoppings[i].promotion = promotionRepository.findAllPromotion().Where(w => w.Id == shoppings[i].shoppingCart.IdPromotion).First();
            }
            return shoppings.AsQueryable();
        }

        public async Task<Guid> Update(ShoppingCart entry)
        {
            await shoppingCartRepository.Update(entry);
            return entry.Id;
        }

        public async Task<Guid> UpdateValue(ShoppingCartRequest entry)
        {
            ShoppingCart shoppingCart = findAll().Where(w => w.Id == new Guid(entry.Id)).FirstOrDefault();
            shoppingCart.Quantity = entry.Quantity;
            shoppingCart.AlterDate = DateTime.Now;
            await shoppingCartRepository.Update(shoppingCart);
            return shoppingCart.Id;
        }

        public async Task<Guid> Inactive(ShoppingCartRequest entry)
        {
            ShoppingCart shoppingCart = findAll().Where(w => w.Id == new Guid(entry.Id)).FirstOrDefault();
            shoppingCart.Status = StatusItem.Inactive;
            shoppingCart.AlterDate = DateTime.Now;
            await shoppingCartRepository.Update(shoppingCart);
            return shoppingCart.Id;
        }

    }

}
