using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(00)]
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
        public DateTime CreationDate { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public int StateId { get; set; }
        [ForeignKey("StateId")]
        public State State { get; set; }

    }
}
