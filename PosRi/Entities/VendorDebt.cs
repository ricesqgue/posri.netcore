using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class VendorDebt
    {
        public int Id { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public decimal Debt { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public int PurchaseHeaderId { get; set; }
        [ForeignKey("PurchaseHeaderId")]
        public PurchaseHeader PurchaseHeader { get; set; }

        public int VendorId { get; set; }
        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; }

        public ICollection<VendorPayment> VendorPayments { get; set; }
    }
}
