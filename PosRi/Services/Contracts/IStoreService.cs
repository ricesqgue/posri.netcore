using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;
using PosRi.Models.Helper;
using PosRi.Models.Request;

namespace PosRi.Services.Contracts
{
    public interface IStoreService
    {
        Task<IEnumerable<Store>> GetStores();

        Task<Store> GetStore(int storeId);

        Task<bool> StoreExists(NewStoreDto newStore);

        Task<int> AddStore(NewStoreDto newStore);

        Task<IEnumerable<UserWithManyToManyRelation>> GetUsersByStore(int storeId);
    }
}
