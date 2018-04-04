using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;
using PosRi.Models.Request.Brand;

namespace PosRi.Services.Contracts
{
    public interface IBrandRepository
    {
        Task<Brand> GetBrandAsync(int id);

        Task<ICollection<Brand>> GetBrandsAsync();

        Task<bool> IsDuplicateBrandAsync(NewBrandDto brand);

        Task<bool> IsDuplicateBrandAsync(EditBrandDto brand);

        Task<bool> BrandExistsAsync(int id);

        Task<int> AddBrandAsync(NewBrandDto newBrand);

        Task<bool> EditBrandAsync(EditBrandDto brand);

        Task<bool> DeleteBrandAsync(int id);
    }
}
