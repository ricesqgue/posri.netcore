using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PosRi.Entities;
using PosRi.Entities.Context;
using PosRi.Models.Request.CashRegister;
using PosRi.Services.Contracts;

namespace PosRi.Services
{
    public class CashRegisterService : ICashRegisterService
    {
        private readonly PosRiContext _dbContext;

        public CashRegisterService(PosRiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CashRegister> GetCashRegister(int id)
        {
            return await _dbContext.CashRegisters.FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<ICollection<CashRegister>> GetCashRegisters(int storeId)
        {
            return await _dbContext.CashRegisters.Where(c => c.StoreId == storeId && c.IsActive).ToListAsync();
        }

        public async Task<bool> IsDuplicateCashRegister(int storeId, NewCashRegisterDto cashRegister)
        {
            return await _dbContext.CashRegisters
                .AnyAsync(c =>
                    c.Name.Equals(cashRegister.Name, StringComparison.InvariantCultureIgnoreCase) &&
                    c.StoreId == storeId && c.IsActive);
        }

        public async Task<bool> IsDuplicateCashRegister(int storeId, EditCashRegisterDto cashRegister)
        {
            return await _dbContext.CashRegisters
                .AnyAsync(c =>
                    c.Name.Equals(cashRegister.Name, StringComparison.InvariantCultureIgnoreCase) &&
                    c.Id != cashRegister.Id &&
                    c.StoreId == storeId && c.IsActive);
        }

        public async Task<bool> CashRegisterExists(int id)
        {
            return await _dbContext.CashRegisters.AnyAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<int> AddCashRegister(int storeId, NewCashRegisterDto newCashRegister)
        {
            var cashRegister = new CashRegister
            {
                StoreId = storeId,
                Name = newCashRegister.Name,
                IsActive = true
            };

            await _dbContext.CashRegisters.AddAsync(cashRegister);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return cashRegister.Id;
            }

            return 0;

        }

        public async Task<bool> EditCashRegister(EditCashRegisterDto cashRegister)
        {
            var editCashRegister = await _dbContext.CashRegisters.FindAsync(cashRegister.Id);

            editCashRegister.Name = cashRegister.Name;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCashRegister(int id)
        {
            var cashRegister = await _dbContext.CashRegisters.FindAsync(id);

            cashRegister.IsActive = false;

            return await _dbContext.SaveChangesAsync() > 0;

        }
    }
}
