using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PosRi.Entities;
using PosRi.Entities.Context;
using PosRi.Models.Request.Client;
using PosRi.Services.Contracts;

namespace PosRi.Services.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly PosRiContext _dbContext;

        public ClientRepository(PosRiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> GetClient(int id)
        {
            return await _dbContext.Clients
                    .Include(c => c.State)
                    .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<ICollection<Client>> GetClients()
        {
            return await _dbContext.Clients
                .Include(c => c.State)
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task<bool> IsDuplicateClient(NewClientDto client)
        {
            return await _dbContext.Clients.AnyAsync(c =>
                (c.Email.Equals(client.Email, StringComparison.InvariantCultureIgnoreCase) || c.Rfc.Equals(client.Rfc, StringComparison.InvariantCultureIgnoreCase)) && c.IsActive);
        }

        public async Task<bool> IsDuplicateClient(EditClientDto client)
        {
            return await _dbContext.Clients.AnyAsync(c =>
                (c.Email.Equals(client.Email, StringComparison.InvariantCultureIgnoreCase) || c.Rfc.Equals(client.Rfc, StringComparison.InvariantCultureIgnoreCase)) && c.Id != client.Id && c.IsActive);
        }

        public async Task<bool> ClientExists(int id)
        {
            return await _dbContext.Clients.AnyAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<int> AddClient(NewClientDto newClient)
        {
            var client = new Client
            {
                Name = newClient.Name,
                City = newClient.City,
                CreationDate = DateTime.Now,
                Email = newClient.Email,
                Phone = newClient.Phone,
                Rfc = newClient.Rfc.ToUpper(),
                Address = newClient.Address,
                StateId = newClient.State.Id,
                IsActive = true
            };

            await _dbContext.Clients.AddAsync(client);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return client.Id;
            }

            return 0;
        }

        public async Task<bool> EditClient(EditClientDto editClient)
        {
            var client = await _dbContext.Clients.FindAsync(editClient.Id);

            client.Name = editClient.Name;
            client.City = editClient.City;
            client.CreationDate = DateTime.Now;
            client.Email = editClient.Email;
            client.Phone = editClient.Phone;
            client.Rfc = editClient.Rfc.ToUpper();
            client.Address = editClient.Address;
            client.StateId = editClient.State.Id;
            client.Name = editClient.Name;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteClient(int id)
        {
            var client = await _dbContext.Clients.FindAsync(id);

            client.IsActive = false;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
