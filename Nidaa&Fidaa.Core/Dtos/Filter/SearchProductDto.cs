using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos.Search
{
    public class SearchProductDto
    {
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public decimal BasePrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string? BasePicture { get; set; }
        public decimal Rating { get; set; }
    }
}
