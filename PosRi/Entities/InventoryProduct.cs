using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class InventoryProduct
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime LastAdd { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public Store Store { get; set; }
    }
}
