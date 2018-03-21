using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class ClientDebt
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

        public int SaleHeaderId { get; set; }
        [ForeignKey("SaleHeaderId")]
        public SaleHeader SaleHeader { get; set; }

        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        public ICollection<ClientPayment> ClientPayments { get; set; } = new List<ClientPayment>();
    }
}
