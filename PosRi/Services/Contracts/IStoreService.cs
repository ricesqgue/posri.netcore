using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;
using PosRi.Models.Helper;
using PosRi.Models.Request.Store;

namespace PosRi.Services.Contracts
{
    public interface IStoreService
    {
        Task<IEnumerable<Store>> GetStores();

        Task<Store> GetStore(int storeId);

        Task<bool> IsDuplicateStore(NewStoreDto newStore);

        Task<bool> IsDuplicateStore(EditStoreDto editStore);

        Task<bool> StoreExists(int id);
            
        Task<int> AddStore(NewStoreDto newStore);

        Task<bool> EditStore(EditStoreDto editStore);

        Task<bool> DeleteStore(int id);
        
        Task<IEnumerable<UserWithManyToManyRelation>> GetUsersByStore(int storeId);
    }
}
