using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;

namespace PosRi.Services.Contracts
{
    public interface ICommonRepository
    {
        Task<ICollection<State>> GetStatesAsync();
    }
}
