using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class ClientPayment
    {
        public int Id { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Cash { get; set; }

        [Required]
        public decimal Card { get; set; }

        public int ClientDebtId { get; set; }
        [ForeignKey("ClientDebtId")]
        public ClientDebt ClientDebt { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
