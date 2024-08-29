using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos.Product
{
    public class UpdateProductSizeDto
    {
        public int Id { get; set; } // ID of the product size to update
        public string Size { get; set; } // New size for the product
        public decimal Price { get; set; } // New price for the product size
    }
}
