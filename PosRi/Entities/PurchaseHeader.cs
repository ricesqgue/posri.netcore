using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class PurchaseHeader
    {
        public int Id { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public decimal SubTotal { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public decimal Discount { get; set; }

        [Required]
        public decimal PaidCash { get; set; }

        [Required]
        public decimal PaidCard { get; set; }

        [Required]
        public decimal PaidCredit { get; set; }

        public int VendorId { get; set; }
        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();
    }
}
