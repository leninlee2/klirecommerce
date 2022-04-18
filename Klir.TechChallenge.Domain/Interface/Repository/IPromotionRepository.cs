using Klir.TechChallenge.Domain.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Domain.Interface.Repository
{
    public interface IPromotionRepository : IRepository<Promotion>
    {
        Task<Guid> AddPromotion(Promotion entry);
        List<Promotion> findAllPromotion();

        Task<Guid> UpdatePromotion(Promotion entry);
    }
}
