using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PosRi.Entities;
using PosRi.Entities.Context;
using PosRi.Models.Request.Size;
using PosRi.Services.Contracts;

namespace PosRi.Services.Repositories
{
    public class SizeRepository : ISizeRepository
    {
        private readonly PosRiContext _dbContext;

        public SizeRepository(PosRiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Size> GetSize(int id)
        {
            return await _dbContext.Sizes
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<ICollection<Size>> GetSizes()
        {
            return await _dbContext.Sizes
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task<bool> IsDuplicateSize(NewSizeDto size)
        {
            return await _dbContext.Sizes.AnyAsync(c =>
                c.Name.Equals(size.Name, StringComparison.InvariantCultureIgnoreCase) && c.IsActive);
        }

        public async Task<bool> IsDuplicateSize(EditSizeDto size)
        {
            return await _dbContext.Sizes.AnyAsync(c =>
                c.Name.Equals(size.Name, StringComparison.InvariantCultureIgnoreCase) && c.Id != size.Id && c.IsActive);
        }

        public async Task<bool> SizeExists(int id)
        {
            return await _dbContext.Sizes.AnyAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<int> AddSize(NewSizeDto newSize)
        {
            var size = new Size
            {
                Name = newSize.Name,
                IsActive = true
            };

            await _dbContext.Sizes.AddAsync(size);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return size.Id;
            }

            return 0;
        }

        public async Task<bool> EditSize(EditSizeDto editSize)
        {
            var size = await _dbContext.Sizes.FindAsync(editSize.Id);

            size.Name = editSize.Name;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteSize(int id)
        {
            var size = await _dbContext.Sizes.FindAsync(id);

            size.IsActive = false;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
