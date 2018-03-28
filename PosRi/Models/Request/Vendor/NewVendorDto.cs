using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;
using PosRi.Models.Response;

namespace PosRi.Models.Request.Vendor
{
    public class NewVendorDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(25)]
        public string Rfc { get; set; }

        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [Required]
        public StateDto State { get; set; }
    }
}
