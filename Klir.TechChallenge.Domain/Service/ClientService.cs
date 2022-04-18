using Klir.TechChallenge.Domain.EntityModel;
using Klir.TechChallenge.Domain.Interface.Repository;
using Klir.TechChallenge.Domain.Interface.Service;
using Klir.TechChallenge.Domain.Model;
using Klir.TechChallenge.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Domain.Service
{
    public class ClientService : IClientService
    {
        private IClientRepository clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public async Task<Guid> Add(Client entry)
        {
            await clientRepository.AddClient(entry);
            return entry.Id;
        }

        public IQueryable<Client> findAll()
        {
            return clientRepository.findAll();
        }

        public Client findAllById(Guid guid)
        {
            return clientRepository.findAll().Where(w => w.Id == guid).FirstOrDefault();
        }

        public Client findByLoginAndPassword(string email, string password)
        {
            var client = clientRepository.findAll().Where(w => w.Email == email && w.Password == password && w.Status==StatusItem.Active).ToList();
            if (client.Count() > 0)
                return client.First();
            else
                return null;
        }

        public Task<Guid> Inactive(Guid entry)
        {
            var client = findAll().Where(w => w.Id == entry).First();
            client.AlterDate = DateTime.Now;
            client.Status = StatusItem.Inactive;
            return Update(client);
        }

        public async Task<Guid> Update(Client entry)
        {
            await clientRepository.UpdateClient(entry);
            return entry.Id;
        }

        public Task<Guid> Update(ClientRequest entry)
        {
            var client = findAll().Where(w => w.Id == new Guid(entry.Id)).First();
            client.LastName = entry.LastName;
            client.PhoneNumber = entry.PhoneNumber;
            client.State = entry.State;
            client.ZipCode = entry.ZipCode;
            client.FirstName = entry.FirstName;
            client.AlterDate = DateTime.Now;
            client.Address = entry.Address;
            client.City = entry.City;
            return Update(client);
        }


    }

}
