using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PosRi.Entities;
using PosRi.Entities.Context;
using PosRi.Models.Request.Vendor;
using PosRi.Services.Contracts;

namespace PosRi.Services.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly PosRiContext _dbContext;

        public VendorRepository(PosRiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Vendor> GetVendor(int id)
        {
            return await _dbContext.Vendors
                    .Include(v => v.State)
                    .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<ICollection<Vendor>> GetVendors()
        {
            return await _dbContext.Vendors
                .Include(v => v.State)
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task<bool> IsDuplicateVendor(NewVendorDto vendor)
        {
            return await _dbContext.Vendors.AnyAsync(c =>
                (c.Email.Equals(vendor.Email, StringComparison.InvariantCultureIgnoreCase) || c.Rfc.Equals(vendor.Rfc, StringComparison.InvariantCultureIgnoreCase)) && c.IsActive);
        }

        public async Task<bool> IsDuplicateVendor(EditVendorDto vendor)
        {
            return await _dbContext.Vendors.AnyAsync(c =>
                (c.Email.Equals(vendor.Email, StringComparison.InvariantCultureIgnoreCase) || c.Rfc.Equals(vendor.Rfc, StringComparison.InvariantCultureIgnoreCase)) && c.Id != vendor.Id && c.IsActive);
        }

        public async Task<bool> VendorExists(int id)
        {
            return await _dbContext.Vendors.AnyAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<int> AddVendor(NewVendorDto newVendor)
        {
            var vendor = new Vendor
            {
                Name = newVendor.Name,
                City = newVendor.City,
                CreationDate = DateTime.Now,
                Email = newVendor.Email,
                Phone = newVendor.Phone,
                Rfc = newVendor.Rfc.ToUpper(),
                Address = newVendor.Address,
                StateId = newVendor.State.Id,
                IsActive = true
            };

            await _dbContext.Vendors.AddAsync(vendor);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return vendor.Id;
            }

            return 0;
        }

        public async Task<bool> EditVendor(EditVendorDto editVendor)
        {
            var vendor = await _dbContext.Vendors.FindAsync(editVendor.Id);

            vendor.Name = editVendor.Name;
            vendor.City = editVendor.City;
            vendor.CreationDate = DateTime.Now;
            vendor.Email = editVendor.Email;
            vendor.Phone = editVendor.Phone;
            vendor.Rfc = editVendor.Rfc.ToUpper();
            vendor.Address = editVendor.Address;
            vendor.StateId = editVendor.State.Id;
            vendor.Name = editVendor.Name;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteVendor(int id)
        {
            var vendor = await _dbContext.Vendors.FindAsync(id);

            vendor.IsActive = false;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
