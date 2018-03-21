using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PosRi.Entities
{
    public class UserStore
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public Store Store { get; set; }
    }
}
