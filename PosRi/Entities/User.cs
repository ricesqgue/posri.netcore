using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(25)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
        public ICollection<UserStore> Stores { get; set; } = new List<UserStore>();

    }
}
