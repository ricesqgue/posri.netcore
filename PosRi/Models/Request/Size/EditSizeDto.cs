using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Models.Request.Size
{
    public class EditSizeDto : NewSizeDto
    {
        [Required]
        public int Id { get; set; }
    }
}
