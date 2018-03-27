using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Models.Request.Category
{
    public class NewSubCategoryDto
    {
        [Required]
        [MaxLength(75)]
        public string Name { get; set; }
    }
}
