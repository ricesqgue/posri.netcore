using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Models.Response
{
    public class ProductHeaderDto
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public SubCategoryDto SubCategory { get; set; }

        public BrandDto Brand { get; set; }

        public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
