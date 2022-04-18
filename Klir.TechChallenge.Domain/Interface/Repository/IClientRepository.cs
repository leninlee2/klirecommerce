using Klir.TechChallenge.Domain.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Domain.Interface.Repository
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Guid> AddClient(Client entry);

        Task<Guid> UpdateClient(Client entry);

        Client findClientById(Guid guid);
    }
}
