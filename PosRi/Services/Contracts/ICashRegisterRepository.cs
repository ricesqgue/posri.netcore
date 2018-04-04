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
        Task<CashRegister> GetCashRegister(int id);

        Task<ICollection<CashRegister>> GetCashRegisters(int storeId);

        Task<bool> IsDuplicateCashRegister(int storeId, NewCashRegisterDto cashRegister);

        Task<bool> IsDuplicateCashRegister(int storeId, EditCashRegisterDto cashRegister);

        Task<bool> CashRegisterExists(int id);

        Task<int> AddCashRegister(int storeId, NewCashRegisterDto newCashRegister);

        Task<bool> EditCashRegister(EditCashRegisterDto cashRegister);

        Task<bool> DeleteCashRegister(int id);
    }
}
