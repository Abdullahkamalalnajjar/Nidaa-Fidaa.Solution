using Nidaa_Fidaa.Core.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos.Shop
{
    public class ProductDto
    {
        public int ProductId { get; set; } // معرّف المنتج
        public string? Title { get; set; } // عنوان المنتج
        public string? Description { get; set; } // وصف المنتج
        public decimal BasePrice { get; set; } // السعر الأساسي
        public decimal DiscountedPrice { get; set; } // السعر المخفض
        public string? BasePicture { get; set; } // رابط الصورة الأساسية للمنتج
        public int CategoryId { get; set; } // معرّف الفئة
        public List<ProductSizeDto> ProductSizes { get; set; } = new List<ProductSizeDto>(); // قائمة الأحجام
        public List<AddProductAdditionDto> ProductAdditions { get; set; } = new List<AddProductAdditionDto>(); // قائمة الإضافات
        public int ShopId { get; set; } // معرّف المتجر
    }
}
