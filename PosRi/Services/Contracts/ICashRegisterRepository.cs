using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;
using PosRi.Models.Request.CashRegister;

namespace PosRi.Services.Contracts
{
    public interface ICashRegisterRepository
    {
        Task<CashRegister> GetCashRegisterAsync(int id);

        Task<ICollection<CashRegister>> GetCashRegistersAsync(int storeId);

        Task<bool> IsDuplicateCashRegisterAsync(int storeId, NewCashRegisterDto cashRegister);

        Task<bool> IsDuplicateCashRegisterAsync(int storeId, EditCashRegisterDto cashRegister);

        Task<bool> CashRegisterExistsAsync(int id);

        Task<int> AddCashRegisterAsync(int storeId, NewCashRegisterDto newCashRegister);

        Task<bool> EditCashRegisterAsync(EditCashRegisterDto cashRegister);

        Task<bool> DeleteCashRegisterAsync(int id);
    }
}
