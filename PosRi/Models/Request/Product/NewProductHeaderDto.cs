using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;
using PosRi.Models.Response;

namespace PosRi.Models.Request.Product
{
    public class NewProductHeaderDto
    {

        [Required]
        [MaxLength(150)]
        public string Model { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        [MaxLength(35)]
        public string ShortDescription { get; set; }

        [Required]
        public SubCategoryDto SubCategory { get; set; }

        [Required]
        public BrandDto Brand { get; set; }

        public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
