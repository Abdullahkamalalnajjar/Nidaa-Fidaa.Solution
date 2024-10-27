using System.Text.Json.Serialization;

namespace Nidaa_Fidaa.Core.Entities
{
    public class Basket
    {
        public int Id { get; set; } 
        public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
        public int? OrderId { get; set; } // Optional if you want to reference an order directly
        [JsonIgnore]
        public Order? Order { get; set; } // Optional if you want to reference an order directly
        public decimal TotalPrice
        {
            get
            {
                // Calculate the total price based on items in the basket
                return Items.Sum(item => item.TotalPrice);
            }
        }
    }
}
