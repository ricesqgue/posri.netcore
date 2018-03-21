using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class SaleHeader
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

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

        [Required] public decimal PaidCredit { get; set; }

        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        public int CashRegisterId { get; set; }
        [ForeignKey("CashRegisterId")]
        public CashRegister CashRegister { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    }
}
