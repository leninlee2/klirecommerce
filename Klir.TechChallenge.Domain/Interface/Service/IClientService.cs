using Klir.TechChallenge.Domain.EntityModel;
using Klir.TechChallenge.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Domain.Interface.Service
{
    public interface IClientService : IService<Client>
    {
        Client findByLoginAndPassword(string email,string password);

        Client findAllById(Guid guid);

        Task<Guid> Inactive(Guid entry);

        Task<Guid> Update(ClientRequest entry);
    }

}
