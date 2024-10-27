using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos.Shop
{
    public class ShopCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductDto> Products { get; set; } = new List<ProductDto>(); // قائمة المنتجات
    }
}
