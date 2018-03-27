using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(75)]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}
