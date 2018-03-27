using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Models.Request.CashRegister
{
    public class NewCashRegisterDto
    {

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
