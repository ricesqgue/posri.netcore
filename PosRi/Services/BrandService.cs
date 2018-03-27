using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PosRi.Entities;
using PosRi.Entities.Context;
using PosRi.Models.Request.Brand;
using PosRi.Services.Contracts;

namespace PosRi.Services
{
    public class BrandService : IBrandService
    {
        private readonly PosRiContext _dbContext;

        public BrandService(PosRiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Brand> GetBrand(int id)
        {
            return await _dbContext.Brands
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<ICollection<Brand>> GetBrands()
        {
            return await _dbContext.Brands
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task<bool> IsDuplicateBrand(NewBrandDto brand)
        {
            return await _dbContext.Brands.AnyAsync(c =>
                c.Name.Equals(brand.Name, StringComparison.InvariantCultureIgnoreCase) && c.IsActive);
        }

        public async Task<bool> IsDuplicateBrand(EditBrandDto brand)
        {
            return await _dbContext.Brands.AnyAsync(c =>
                c.Name.Equals(brand.Name, StringComparison.InvariantCultureIgnoreCase) && c.Id != brand.Id && c.IsActive);
        }

        public async Task<bool> BrandExists(int id)
        {
            return await _dbContext.Brands.AnyAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<int> AddBrand(NewBrandDto newBrand)
        {
            var brand = new Brand
            {
                Name = newBrand.Name,
                IsActive = true
            };

            await _dbContext.Brands.AddAsync(brand);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return brand.Id;
            }

            return 0;
        }

        public async Task<bool> EditBrand(EditBrandDto editBrand)
        {
            var brand = await _dbContext.Brands.FindAsync(editBrand.Id);

            brand.Name = editBrand.Name;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBrand(int id)
        {
            var brand = await _dbContext.Brands.FindAsync(id);

            brand.IsActive = false;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
