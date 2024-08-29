namespace Nidaa_Fidaa.Core.Entities
{
    public class Basket
    {
        public int Id { get; set; }
        public int CustomerId { get; set; } 
        public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();


    }
}