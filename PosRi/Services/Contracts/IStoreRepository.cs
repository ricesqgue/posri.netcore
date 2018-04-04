using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;
using PosRi.Models.Helper;
using PosRi.Models.Request.Store;

namespace PosRi.Services.Contracts
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetStoresAsync();

        Task<Store> GetStoreAsync(int storeId);

        Task<bool> IsDuplicateStoreAsync(NewStoreDto newStore);

        Task<bool> IsDuplicateStoreAsync(EditStoreDto editStore);

        Task<bool> StoreExistsAsync(int id);
            
        Task<int> AddStoreAsync(NewStoreDto newStore);

        Task<bool> EditStoreAsync(EditStoreDto editStore);

        Task<bool> DeleteStoreAsync(int id);
        
        Task<IEnumerable<UserWithManyToManyRelation>> GetUsersByStoreAsync(int storeId);
    }
}
