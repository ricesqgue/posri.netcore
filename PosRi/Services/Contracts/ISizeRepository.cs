using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;
using PosRi.Models.Request.Size;

namespace PosRi.Services.Contracts
{
    public interface ISizeRepository
    {
        Task<Size> GetSizeAsync(int id);

        Task<ICollection<Size>> GetSizesAsync();

        Task<bool> IsDuplicateSizeAsync(NewSizeDto size);

        Task<bool> IsDuplicateSizeAsync(EditSizeDto size);

        Task<bool> SizeExistsAsync(int id);

        Task<int> AddSizeAsync(NewSizeDto newSize);

        Task<bool> EditSizeAsync(EditSizeDto size);

        Task<bool> DeleteSizeAsync(int id);
    }
}
