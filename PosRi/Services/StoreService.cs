using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PosRi.Entities;
using PosRi.Entities.Context;
using PosRi.Models.Helper;
using PosRi.Models.Request;
using PosRi.Models.Response;

namespace PosRi.Services.Contracts
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

        public async Task<bool> StoreExists(NewStoreDto newStore)
        {
            return await _dbContext.Stores.AnyAsync(store => store.Name == newStore.Name && store.IsActive);
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
