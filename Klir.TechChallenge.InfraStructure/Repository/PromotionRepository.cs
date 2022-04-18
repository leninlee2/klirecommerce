using Klir.TechChallenge.InfraStructure.ContextModel;
using Klir.TechChallenge.Domain.EntityModel;
using Klir.TechChallenge.InfraStructure.Repository.EntityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Klir.TechChallenge.Domain.Interface.Repository;

namespace Klir.TechChallenge.InfraStructure.Repository
{
    public class PromotionRepository : EntityRepository<Promotion>, IPromotionRepository
    {
        private readonly KlirContext context;

        public PromotionRepository(KlirContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Guid> AddPromotion(Promotion entry)
        {
            await base.Add(entry);
            return entry.Id;
        }

        public List<Promotion> findAllPromotion()
        {
            return base.findAll().ToList();
        }

        public async Task<Guid> UpdatePromotion(Promotion entry)
        {
            await base.Update(entry);
            return entry.Id;
        }

    }
}
