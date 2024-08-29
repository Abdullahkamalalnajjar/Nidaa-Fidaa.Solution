using Nidaa_Fidaa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos.Basket
{
    public class BasketDto
    {
        public int CustomerId { get; set; }
        public List<BasketItemDto> Items { get; set; } 
    }
}
