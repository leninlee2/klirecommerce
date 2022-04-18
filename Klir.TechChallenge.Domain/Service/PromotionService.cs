using Klir.TechChallenge.Domain.Interface.Service;
using Klir.TechChallenge.Domain.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Klir.TechChallenge.Domain.Interface.Repository;

namespace Klir.TechChallenge.Domain.Service
{
    public class PromotionService : IPromotionService
    {
        private IPromotionRepository promotionRepository;

        public PromotionService(IPromotionRepository promotionRepository)
        {
            this.promotionRepository = promotionRepository;
        }

        public async Task<Guid> Add(Promotion entry)
        {
           await promotionRepository.AddPromotion(entry);
           return entry.Id;
        }

        public IQueryable<Promotion> findAll()
        {
            return promotionRepository.findAll();
        }

        public async Task<Guid> Update(Promotion entry)
        {
            await promotionRepository.UpdatePromotion(entry);
            return entry.Id;
        }

    }
}
