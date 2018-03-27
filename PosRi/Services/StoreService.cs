using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PosRi.Entities;
using PosRi.Entities.Context;
using PosRi.Models.Helper;
using PosRi.Models.Request.Store;
using PosRi.Services.Contracts;

namespace PosRi.Services
{
    public class StoreService : IStoreService
    {
        private readonly PosRiContext _dbContext;

        public StoreService(PosRiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Store>> GetStores()
        {
            return await _dbContext.Stores.Where(store => store.IsActive).ToListAsync();
        }

        public async Task<Store> GetStore(int storeId)
        {
            return await _dbContext.Stores.FirstOrDefaultAsync(store => store.Id == storeId && store.IsActive);
        }

        public async Task<bool> IsDuplicateStore(NewStoreDto newStore)
        {
            return await _dbContext.Stores.AnyAsync(store => store.Name == newStore.Name && store.IsActive);
        }

        public async Task<bool> IsDuplicateStore(EditStoreDto editStore)
        {
            return await _dbContext.Stores.AnyAsync(store => store.Name == editStore.Name && store.Id != editStore.Id && store.IsActive);
        }

        public async Task<bool> StoreExists(int id)
        {
            return await _dbContext.Stores.AnyAsync(store => store.Id == id && store.IsActive);
        }

        public async Task<int> AddStore(NewStoreDto newStore)
        {
            var store = new Store
            {
                Name = newStore.Name,
                Address = newStore.Address,
                Phone = newStore.Phone,
                IsActive = true
            };

            await _dbContext.Stores.AddAsync(store);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return store.Id;
            }

            return 0;
        }

        public async Task<bool> EditStore(EditStoreDto editStore)
        {
            var store = await _dbContext.Stores.FindAsync(editStore.Id);
            store.Name = editStore.Name;
            store.Address = editStore.Address;
            store.Phone = editStore.Phone;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteStore(int id)
        {
            var store = await _dbContext.Stores.FindAsync(id);
            store.IsActive = false;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<UserWithManyToManyRelation>> GetUsersByStore(int storeId)
        {
            return await _dbContext.Users
                .Where(u => u.IsActive && u.Stores.Any(store => store.StoreId == storeId))
                .OrderBy(u => u.Name)
                .Select(user => new UserWithManyToManyRelation()
                {
                    Id = user.Id,
                    Birthday = user.Birthday,
                    HireDate = user.HireDate,
                    Name = user.Name,
                    Username = user.Username,
                    Password = user.Password,
                    Roles = user.Roles.Select(role => role.Role).ToList(),
                    Stores = user.Stores.Select(store => store.Store).ToList()
                })
                .ToListAsync();
        }
    }
}
