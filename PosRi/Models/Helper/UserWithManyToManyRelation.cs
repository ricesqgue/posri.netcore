using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;

namespace PosRi.Models.Helper
{
    public class UserWithManyToManyRelation
    {
 
        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Role> Roles { get; set; } = new List<Role>();
        public ICollection<Store> Stores { get; set; } = new List<Store>();
    }
}
