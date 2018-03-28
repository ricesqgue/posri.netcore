using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Models.Request.Client
{
    public class EditClientDto : NewClientDto
    {
        [Required]
        public int Id { get; set; }
    }
}
