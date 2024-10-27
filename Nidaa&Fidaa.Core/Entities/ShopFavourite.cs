using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Entities
{
    public class ShopFavourite
    {
        public int Id { get; set; }
        public bool? IsFavourite { get; set; }

        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }

       
        public int? ShopId { get; set; }
        [JsonIgnore]
        public Shop Shop { get; set; }
    }
}
