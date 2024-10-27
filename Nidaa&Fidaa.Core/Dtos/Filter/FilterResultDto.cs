using Nidaa_Fidaa.Core.Dtos.Product;
using Nidaa_Fidaa.Core.Dtos.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos.Search
{
    public class FilterResultDto
    {
        public List<ShopViewByid> Shops { get; set; } = new List<ShopViewByid>();
        public List<ProductViewDtoByid> Products { get; set; } = new List<ProductViewDtoByid>();
    }
}
