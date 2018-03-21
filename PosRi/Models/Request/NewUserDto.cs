using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Models.Request
{
    public class NewUserDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        public ICollection<int> Roles { get; set; }

        public ICollection<int> Stores { get; set; } = new List<int> { 1 };
    }
}
