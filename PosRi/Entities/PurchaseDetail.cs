using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class PurchaseDetail
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal SalePrice { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int PurchaseHeaderId { get; set; }
        [ForeignKey("PurchaseHeaderId")]
        public PurchaseHeader PurchaseHeader { get; set; }
    }
}
