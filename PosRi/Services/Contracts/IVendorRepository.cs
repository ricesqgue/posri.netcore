using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;
using PosRi.Models.Request.Vendor;

namespace PosRi.Services.Contracts
{
    public interface IVendorRepository
    {
        Task<Vendor> GetVendorAsync(int id);

        Task<ICollection<Vendor>> GetVendorsAsync();

        Task<bool> IsDuplicateVendorAsync(NewVendorDto vendor);

        Task<bool> IsDuplicateVendorAsync(EditVendorDto vendor);

        Task<bool> VendorExistsAsync(int id);

        Task<int> AddVendorAsync(NewVendorDto newVendor);

        Task<bool> EditVendorAsync(EditVendorDto vendor);

        Task<bool> DeleteVendorAsync(int id);
    }
}
