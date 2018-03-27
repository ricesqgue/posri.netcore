using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;
using PosRi.Models.Request.Brand;

namespace PosRi.Services.Contracts
{
    public interface IBrandService
    {
        Task<Brand> GetBrand(int id);

        Task<ICollection<Brand>> GetBrands();

        Task<bool> IsDuplicateBrand(NewBrandDto brand);

        Task<bool> IsDuplicateBrand(EditBrandDto brand);

        Task<bool> BrandExists(int id);

        Task<int> AddBrand(NewBrandDto newBrand);

        Task<bool> EditBrand(EditBrandDto brand);

        Task<bool> DeleteBrand(int id);
    }
}
