using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Entities
{
  
        public class Trader
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Governorate { get; set; }
            public string Municipality { get; set; }
            public string? CommercialRegistrationNumber { get; set; } // رقم السجل التجاري

            public string TradeActivityName { get; set; }



        // Navigation property for the one-to-many relationship
        public ICollection<Shop> Shops { get; set; } = new List<Shop>(); public int ShopId { get; set; }

        }
    

}
