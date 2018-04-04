using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;
using PosRi.Models.Request.Color;

namespace PosRi.Services.Contracts
{
    public interface IColorRepository
    {
        Task<Color> GetColorAsync(int id);

        Task<ICollection<Color>> GetColorsAsync();

        Task<bool> IsDuplicateColorAsync(NewColorDto color);

        Task<bool> IsDuplicateColorAsync(EditColorDto color);

        Task<bool> ColorExistsAsync(int id);

        Task<int> AddColorAsync(NewColorDto newColor);

        Task<bool> EditColorAsync(EditColorDto color);

        Task<bool> DeleteColorAsync(int id);
    }
}
