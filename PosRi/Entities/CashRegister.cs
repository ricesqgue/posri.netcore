using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class CashRegister
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public Store Store { get; set; }
    }
}
