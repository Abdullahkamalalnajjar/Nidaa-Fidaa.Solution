using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos.Product
{
    public class UpdateProductAdditionDto
    {
        public int Id { get; set; } // ID of the product addition to update
        public string Name { get; set; } // New name for the product addition
        public decimal Price { get; set; } // New price for the product addition
    }

}
