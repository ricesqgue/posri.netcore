using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class ProductHeader
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Code { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        [MaxLength(35)]
        public string ShortDescription { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
