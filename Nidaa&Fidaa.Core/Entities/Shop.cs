using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Entities
{
    public class Shop
    {
        public int Id { get; set; }
        public string? PhotoUrl { get; set; } 
        public string? BaseShopPhotoUrl { get; set; } 
        public string? Location { get; set; } 
        public string? BusinessName { get; set; } 
        public string? BusinessType { get; set; } 
        public decimal Rating { get; set; } = 0.0m;
        public decimal DeliveryTime { get; set; }
        public decimal DeliveryPrice { get; set; } = 0.0m;
        // many to many 
        public ICollection<ShopCategory> ShopCategory { get; set; } = new List<ShopCategory>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<ShopFavourite> ShopFavourites { get; set; } = new List<ShopFavourite>();


        // Foreign key for the relationship
        public int TraderId { get; set; }


    }

}
