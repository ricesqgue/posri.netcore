using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class Role
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<UserRole> Users { get; set; } = new List<UserRole>();

        public Role()
        {
            
        }

        private Role(RoleNames @enum)
        {
            Id = (int) @enum;
            Name = @enum.ToString();
        }

        public static implicit operator RoleNames(Role role)
        {
            return (RoleNames) role.Id;
        }

        public static implicit operator Role(RoleNames @enum)
        {
            return new Role(@enum);
        }

        public enum RoleNames
        {
            SuperAdmin = 1,
            Administrator = 2
        }
    }
}
