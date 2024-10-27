using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image {  get; set; }
        public ICollection<ShopCategory> ShopCategory { get; set; } = new List<ShopCategory>();
        // Collection of ShopCategory to represent many-to-many relationship with Shop

        // Collection of Products related to this Category
        public ICollection<Product> Products { get; set; } = new List<Product>();


    }

}
