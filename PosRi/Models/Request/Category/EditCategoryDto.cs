using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Models.Request.Category
{
    public class EditCategoryDto : NewCategoryDto
    {
        [Required]
        public int Id { get; set; }
    }
}
