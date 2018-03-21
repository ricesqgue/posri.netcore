using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;

namespace PosRi.Models.Response
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime HireDate { get; set; }

        public ICollection<RoleDto> Roles { get; set; }
    }
}
