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
    public class ClientRepository : EntityRepository<Client>, IClientRepository
    {
        private readonly KlirContext context;

        public ClientRepository(KlirContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Guid> AddClient(Client entry)
        {
            await base.Add(entry);
            return entry.Id;
        }

        public Client findClientById(Guid guid)
        {
            var result = base.findAll().Where(w => w.Id == guid).ToList();
            if (result.Count() > 0)
                return result.First();
            else
                return null;
        }

        public async Task<Guid> UpdateClient(Client entry)
        {
            await base.Update(entry);
            return entry.Id;
        }

    }

}
