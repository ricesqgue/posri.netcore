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
        Task<Vendor> GetVendor(int id);

        Task<ICollection<Vendor>> GetVendors();

        Task<bool> IsDuplicateVendor(NewVendorDto vendor);

        Task<bool> IsDuplicateVendor(EditVendorDto vendor);

        Task<bool> VendorExists(int id);

        Task<int> AddVendor(NewVendorDto newVendor);

        Task<bool> EditVendor(EditVendorDto vendor);

        Task<bool> DeleteVendor(int id);
    }
}
