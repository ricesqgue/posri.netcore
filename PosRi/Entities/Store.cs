using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class Store
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(30)]
        public string Phone { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public ICollection<UserStore> Users { get; set; } = new List<UserStore>();

    }
}
