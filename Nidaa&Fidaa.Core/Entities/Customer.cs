using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }

        public string? Governorate { get; set; }

        public string? Zone { get; set; }
        public ICollection<ProductFavourite> ProductFavourites { get; set; } = new List<ProductFavourite>();
        public ICollection<ShopFavourite> ShopFavourites { get; set; } = new List<ShopFavourite>();

        [JsonIgnore]
        public Basket? Basket { get; set; }
    }
}
