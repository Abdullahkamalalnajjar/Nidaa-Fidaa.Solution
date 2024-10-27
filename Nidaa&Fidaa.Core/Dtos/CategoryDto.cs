using Microsoft.AspNetCore.Http;
using Nidaa_Fidaa.Core.Dtos.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
