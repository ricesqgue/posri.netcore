using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public ICollection<VendorBrand> Vendors { get; set; } = new List<VendorBrand>();

    }
}
