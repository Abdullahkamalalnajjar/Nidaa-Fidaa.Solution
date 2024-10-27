using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Nidaa_Fidaa.Core.Entities.Identity

{
    public class CustomerIdentity : IdentityUser
    {
        // الربط مع كيان Customer
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }

        public string? Governorate { get; set; }

        public string? Zone { get; set; }
        public ICollection<ProductFavourite> ProductFavourites { get; set; } = new List<ProductFavourite>();
        public ICollection<ShopFavourite> ShopFavourites { get; set; } = new List<ShopFavourite>();

       // [JsonIgnore]
    //    public Basket? Basket { get; set; }
    }
}
