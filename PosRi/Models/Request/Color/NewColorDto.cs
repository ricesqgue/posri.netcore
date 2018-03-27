using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Models.Request.Color
{
    public class NewColorDto
    { 
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(7)]
        [Required]
        public string RgbHex { get; set; }
    }
}
