using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Entities
{
    public class ProductFavourite
    {
        public int Id { get; set; }
        public bool? IsFavourite { get; set; }

        // العلاقات مع العملاء
        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }

        // العلاقات مع المنتجات (Product)
        public int? ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }

  
    }
    

}
