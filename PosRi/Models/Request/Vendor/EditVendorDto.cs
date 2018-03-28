using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Models.Request.Vendor
{
    public class EditVendorDto : NewVendorDto
    {
        [Required]
        public int Id { get; set; }
    }
}
