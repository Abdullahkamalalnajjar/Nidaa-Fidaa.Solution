using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos.Order
{
    public class CreateOrderDto
    { 
        public int CustomerId { get; set; }
        public int ShopId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}
