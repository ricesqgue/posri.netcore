using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Models.Response;

namespace PosRi.Models.Request.Product
{
    public class NewProductDto
    {
        [Required]
        [MaxLength(150)]
        public string Code { get; set; }

        [MaxLength(150)]
        public string ExtraDescription { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal SpecialPrice { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public SizeDto Size { get; set; }

        public ColorDto ColorPrimary { get; set; }

        public ColorDto ColorSecondary { get; set; }

    }
}
