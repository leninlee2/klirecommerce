using Klir.TechChallenge.Domain.EntityModel;
using Klir.TechChallenge.Domain.Interface.Controller;
using Klir.TechChallenge.Domain.Interface.Service;
using Klir.TechChallenge.Domain.Model;
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
    public class ClientController : ControllerBase, IClientController
    {
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet("{id}")]
        public Client Get(string id)
        {
            return clientService.findAllById(new Guid(id));
        }

        [HttpGet("authenticate/{login}/{password}")]
        public Client Authenticate(string login,string password)
        {
            return clientService.findByLoginAndPassword(login, password);
        }

        [HttpPost]
        public Task<Guid> Post(ClientRequest client)
        {
            Client entry = new Client()
            {
                Id = Guid.NewGuid(),
                Address=client.Address,
                City=client.City,
                CreateDate=DateTime.Now,
                Email=client.Email,
                FirstName=client.FirstName,
                LastName=client.LastName,
                Password=client.Password,
                PhoneNumber=client.PhoneNumber,
                State=client.State,
                Status=StatusItem.Active,
                ZipCode=client.ZipCode
            };
            return clientService.Add(entry);
        }

        [HttpPost("inactive")]
        public Task<Guid> Inactive(ClientRequest client)
        {
            return clientService.Inactive(new Guid(client.Id));
        }

        [HttpPost("update")]
        public Task<Guid> Update(ClientRequest client)
        {
            return clientService.Update(client);
        }

    }

}
