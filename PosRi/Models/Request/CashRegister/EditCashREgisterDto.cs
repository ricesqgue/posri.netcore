using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Models.Request.CashRegister
{
    public class EditCashRegisterDto : NewCashRegisterDto
    {
        [Required]
        public int Id  { get; set; }
    }
}
