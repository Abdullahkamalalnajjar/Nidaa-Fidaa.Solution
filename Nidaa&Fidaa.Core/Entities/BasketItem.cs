using System.Text.Json.Serialization;

namespace Nidaa_Fidaa.Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
      //  public decimal UnitPrice { get; set; }
        [JsonIgnore]
        public Basket Basket { get; set; }

        public Product Product { get; set; }
        public List<ProductAddition>? Additions { get; set; } = new List<ProductAddition>();

        public int ProductSizeId { get; set; }
       

        public ProductSize ProductSize { get; set; }

        public decimal TotalPrice
        {
            get
            {
                if ( Product==null )
                    return 0;

                // Determine the price to use based on the presence of ProductSize
                decimal basePrice;
                decimal priceToUse;

                if ( ProductSize!=null )
                {
                    // Use the size-specific price if ProductSize is present
                    basePrice=ProductSize.Price;
                }
                else
                {
                    // Use the base price if ProductSize is not present
                    basePrice=Product.BasePrice;
                }

                // Calculate the price with the discount
                var discountPercentage = Product.DiscountedPrice/100m;
                priceToUse=basePrice*(1-discountPercentage);

                // Calculate additions price
                var additionsPrice = Additions?.Sum(addition => addition.Price)??0;

                // Calculate the total price for one item
                var pricePerItem = priceToUse+additionsPrice;

                // Return the total price after applying the quantity
                return pricePerItem*Quantity;
            }
        }

    


    public string? Note { get; set; }


    }

}
