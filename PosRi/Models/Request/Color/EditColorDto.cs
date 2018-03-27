using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Models.Request.Color
{
    public class EditColorDto : NewColorDto
    {
        [Required]
        public int Id { get; set; }
    }
}
