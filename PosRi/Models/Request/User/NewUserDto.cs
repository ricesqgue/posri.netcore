using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PosRi.Models.Response;

namespace PosRi.Models.Request.User
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
        public ICollection<RoleDto> Roles { get; set; }

        public ICollection<StoreDto> Stores { get; set; }
    }
}
