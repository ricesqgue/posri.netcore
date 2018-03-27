using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class Color
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(6)]
        [Required]
        public string RgbHex { get; set; }


        [Required]
        public bool IsActive { get; set; } = true;

    }
}
