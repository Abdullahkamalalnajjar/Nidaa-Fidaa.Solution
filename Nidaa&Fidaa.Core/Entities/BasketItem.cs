using System.Text.Json.Serialization;

namespace Nidaa_Fidaa.Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        [JsonIgnore]
        public Basket Basket { get; set; }
        [JsonIgnore]

        public Product Product { get; set; }
    }

}
