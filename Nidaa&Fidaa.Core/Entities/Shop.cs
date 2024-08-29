using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Entities
{
    public class Shop
    {
        public int Id { get; set; }
     //   public string MerchantPhotoUrl { get; set; } // مسار الصورة المحل
        public string ShopPhotoUrl { get; set; } // مسار الصورة محل المحل
        public string Location { get; set; } 
        public string BusinessName { get; set; } // اسم النشاط التجاري
        public string BusinessType { get; set; } // نوع النشاط

        // many to many 
        public ICollection<ShopCategory> ShopCategory { get; set; } = new List<ShopCategory>();
        public ICollection<Product> Products { get; set; } = new List<Product>();

        // Foreign key for the relationship
        public int TraderId { get; set; }


    }

}
