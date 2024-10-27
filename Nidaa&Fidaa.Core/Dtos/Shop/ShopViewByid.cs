using Nidaa_Fidaa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos.Shop
{
    public class ShopViewByid
    {
       
        
            public int Id { get; set; } // معرّف المتجر
            public string? PhotoUrl { get; set; } // رابط الصورة
            public string? BaseShopPhotoUrl { get; set; } // رابط الصورة الأساسية
            public string? Location { get; set; } // الموقع
            public string? BusinessName { get; set; } // اسم النشاط التجاري
            public string? BusinessType { get; set; } // نوع النشاط
            public List<ShopCategoryDto> ShopCategories { get; set; } = new List<ShopCategoryDto>(); // قائمة الفئات
            public List<ProductDto> Products { get; set; } = new List<ProductDto>(); // قائمة المنتجات
            public int TraderId { get; set; } // معرّف التاجر
            public bool IsFavourite { get; set; }
            public decimal Rating { get; set; }
            public decimal DeliveryTime { get; set; }
            public decimal DeliveryPrice { get; set; }

    }
}
