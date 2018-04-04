using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PosRi.Entities;
using PosRi.Entities.Context;
using PosRi.Services.Contracts;

namespace PosRi.Services.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly PosRiContext _dbContext;

        public CommonRepository(PosRiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<State>> GetStates()
        {
            return await _dbContext.States.ToListAsync();
        }
    }
}
