using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos.Search
{
    public class SearchShopDto
    {
        public int ShopId { get; set; }
        public string? BusinessName { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Location { get; set; }
        public decimal Rating { get; set; }
    }
}
