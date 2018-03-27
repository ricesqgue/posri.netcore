using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Code { get; set; }

        [MaxLength(150)]
        public string ExtraDescription { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public decimal PurchasePrice { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal SpecialPrice { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public int SizeId { get; set; }
        [ForeignKey("SizeId")]
        public Size Size { get; set; }

        public int ColorPrimaryId { get; set; }
        [ForeignKey("ColorPrimaryId")]
        public Color ColorPrimary { get; set; }

        public int ColorSecondaryId { get; set; }
        [ForeignKey("ColorSecondaryId")]
        public Color ColorSecondary { get; set; }

        public int ProductHeaderId { get; set; }
        [ForeignKey("ProductHeaderId")]
        public ProductHeader ProductHeader { get; set; }

    }
}
