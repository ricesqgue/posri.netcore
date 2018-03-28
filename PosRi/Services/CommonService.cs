using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PosRi.Entities;
using PosRi.Entities.Context;
using PosRi.Services.Contracts;

namespace PosRi.Services
{
    public class CommonService : ICommonService
    {
        private readonly PosRiContext _dbContext;

        public CommonService(PosRiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<State>> GetStates()
        {
            return await _dbContext.States.ToListAsync();
        }
    }
}
