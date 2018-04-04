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
        Task<Client> GetClient(int id);

        Task<ICollection<Client>> GetClients();

        Task<bool> IsDuplicateClient(NewClientDto client);

        Task<bool> IsDuplicateClient(EditClientDto client);

        Task<bool> ClientExists(int id);

        Task<int> AddClient(NewClientDto newClient);

        Task<bool> EditClient(EditClientDto client);

        Task<bool> DeleteClient(int id);
    }

}
