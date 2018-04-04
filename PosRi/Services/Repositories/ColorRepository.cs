using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PosRi.Entities;
using PosRi.Entities.Context;
using PosRi.Models.Request.Color;
using PosRi.Services.Contracts;

namespace PosRi.Services.Repositories
{
    public class ColorRepository : IColorRepository
    {
        private readonly PosRiContext _dbContext;

        public ColorRepository(PosRiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Color> GetColorAsync(int id)
        {
            return await _dbContext.Colors
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<ICollection<Color>> GetColorsAsync()
        {
            return await _dbContext.Colors
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task<bool> IsDuplicateColorAsync(NewColorDto color)
        {
            return await _dbContext.Colors.AnyAsync(c =>
                (c.Name.Equals(color.Name, StringComparison.InvariantCultureIgnoreCase) 
                || c.RgbHex.Equals(color.RgbHex, StringComparison.InvariantCultureIgnoreCase))
                && c.IsActive);
        }

        public async Task<bool> IsDuplicateColorAsync(EditColorDto color)
        {
            return await _dbContext.Colors.AnyAsync(c =>
                (c.Name.Equals(color.Name, StringComparison.InvariantCultureIgnoreCase)
                 || c.RgbHex.Equals(color.RgbHex, StringComparison.InvariantCultureIgnoreCase)) && c.Id != color.Id && c.IsActive);
        }

        public async Task<bool> ColorExistsAsync(int id)
        {
            return await _dbContext.Colors.AnyAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<int> AddColorAsync(NewColorDto newColor)
        {
            var color = new Color
            {
                Name = newColor.Name,
                RgbHex = newColor.RgbHex.ToUpper(),
                IsActive = true
            };

            await _dbContext.Colors.AddAsync(color);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return color.Id;
            }

            return 0;
        }

        public async Task<bool> EditColorAsync(EditColorDto editColor)
        {
            var color = await _dbContext.Colors.FindAsync(editColor.Id);

            color.Name = editColor.Name;
            color.RgbHex = editColor.RgbHex.ToUpper();

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteColorAsync(int id)
        {
            var color = await _dbContext.Colors.FindAsync(id);

            color.IsActive = false;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
