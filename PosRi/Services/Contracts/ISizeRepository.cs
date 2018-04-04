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
        Task<Size> GetSize(int id);

        Task<ICollection<Size>> GetSizes();

        Task<bool> IsDuplicateSize(NewSizeDto size);

        Task<bool> IsDuplicateSize(EditSizeDto size);

        Task<bool> SizeExists(int id);

        Task<int> AddSize(NewSizeDto newSize);

        Task<bool> EditSize(EditSizeDto size);

        Task<bool> DeleteSize(int id);
    }
}
