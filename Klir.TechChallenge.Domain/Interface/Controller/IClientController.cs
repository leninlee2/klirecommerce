using Klir.TechChallenge.Domain.EntityModel;
using Klir.TechChallenge.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Domain.Interface.Controller
{
    public interface IClientController
    {
        Client Get(string id);

        Client Authenticate(string login, string password);

        Task<Guid> Post(ClientRequest client);

        Task<Guid> Inactive(ClientRequest client);

        Task<Guid> Update(ClientRequest client);
    }
}
