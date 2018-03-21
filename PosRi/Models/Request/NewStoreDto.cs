using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;

namespace PosRi.Models.Request
{
    public class NewStoreDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        [Required]
        public string Address { get; set; }

        [MaxLength(30)]
        public string Phone { get; set; }
    }
}
