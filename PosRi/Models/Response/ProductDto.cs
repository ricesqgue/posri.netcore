using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosRi.Models.Response
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string ExtraDescription { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal Price { get; set; }

        public decimal SpecialPrice { get; set; }

        public DateTime CreateDate { get; set; }

        public SizeDto Size { get; set; }

        public ColorDto ColorPrimary { get; set; }

        public ColorDto ColorSecondary { get; set; }

    }
}
