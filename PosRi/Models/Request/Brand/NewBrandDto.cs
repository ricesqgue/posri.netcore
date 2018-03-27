using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;

namespace PosRi.Models.Request.Brand
{
    public class NewBrandDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

    }
}
