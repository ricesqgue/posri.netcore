using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class CashRegisterMove
    {
        public int Id { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public decimal Quantity { get; set; }

        public int CashRegisterId { get; set; }
        [ForeignKey("CashRegisterId")]
        public CashRegister CashRegister { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }


    }
}
