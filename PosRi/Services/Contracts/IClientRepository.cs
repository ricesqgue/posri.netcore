using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;
using PosRi.Models.Request.Client;

namespace PosRi.Services.Contracts
{
    public interface IClientRepository
    {
        Task<Client> GetClientAsync(int id);

        Task<ICollection<Client>> GetClientsAsync();

        Task<bool> IsDuplicateClientAsync(NewClientDto client);

        Task<bool> IsDuplicateClientAsync(EditClientDto client);

        Task<bool> ClientExistsAsync(int id);

        Task<int> AddClientAsync(NewClientDto newClient);

        Task<bool> EditClientAsync(EditClientDto client);

        Task<bool> DeleteClientAsync(int id);
    }

}
