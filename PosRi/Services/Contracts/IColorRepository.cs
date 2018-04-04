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
        Task<Color> GetColor(int id);

        Task<ICollection<Color>> GetColors();

        Task<bool> IsDuplicateColor(NewColorDto color);

        Task<bool> IsDuplicateColor(EditColorDto color);

        Task<bool> ColorExists(int id);

        Task<int> AddColor(NewColorDto newColor);

        Task<bool> EditColor(EditColorDto color);

        Task<bool> DeleteColor(int id);
    }
}
