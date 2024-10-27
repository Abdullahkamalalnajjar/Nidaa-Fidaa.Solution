using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos.Favourite
{
    public class ProductWithFavoriteDto
    {
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string? BasePicture { get; set; }
        public decimal? Rating { get; set; }
        public bool IsFavorite { get; set; }
    }
}
