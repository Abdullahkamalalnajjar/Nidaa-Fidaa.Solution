using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nidaa_Fidaa.Core.Entities
{
    public class Order
    {
  

        public int Id { get; set; }

        public string? Location { get; set; }
        public int CustomerId { get; set; }
   
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public decimal TotalAmount { get; set; }
        
        public string Status { get; set; } = "Pending";
    }
}
