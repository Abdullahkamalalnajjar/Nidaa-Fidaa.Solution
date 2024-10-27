using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos.Shop
{
    public class ShopWithFavoriteDto
    {
        public int Id { get; set; }
        public string? PhotoUrl { get; set; }
        public string? BaseShopPhotoUrl { get; set; }
        public string? Location { get; set; }
        public string? BusinessName { get; set; }
        public string? BusinessType { get; set; }
        public decimal Rating { get; set; } 
        public decimal DeliveryTime { get; set; }
        public decimal DeliveryPrice { get; set; } 
        public bool IsFavorite { get; set; }
        public List<ShopCategoryDto> ShopCategories { get; set; } = new List<ShopCategoryDto>();

    }

}
