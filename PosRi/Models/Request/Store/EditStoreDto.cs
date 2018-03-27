using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Models.Request.Store
{
    public class EditStoreDto : NewStoreDto
    {
        [Required]
        public int Id { get; set; }
    }
}
