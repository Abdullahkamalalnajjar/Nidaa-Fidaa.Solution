using Nidaa_Fidaa.Core.Dtos.Favourite;
using Nidaa_Fidaa.Core.Dtos.ProductFavourite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos.Product
{
    public class ProductViewDtoByid
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string BasePicture { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } // Add this property
        public decimal Rating { get; set; } 
        public decimal DeliveryTime { get; set; }
        public decimal DeliveryPrice { get; set; } 
        public List<ProductSizeViewByid> ProductSizes { get; set; }
        public List<ProductAdditionViewByid> ProductAdditions { get; set; }
        public int ShopId { get; set; }
        public List<ProductFavouriteDto> Favourites { get; set; }
    }
}
