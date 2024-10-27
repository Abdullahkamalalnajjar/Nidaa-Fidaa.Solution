using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nidaa_Fidaa.Core.Entities;
namespace Nidaa_Fidaa.Core.Dtos.Shop
{
    public class ShopViewDto
    {
        public int Id { get; set; }
     //   public string MerchantPhotoUrl { get; set; }
        public string? BaseShopPhotoUrl { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Location { get; set; }
        public string? BusinessName { get; set; }
        public string? BusinessType { get; set; }
        public List<ShopCategoryDto> SelectCatergoryIds { get; set; }  // قائمة معرفات الأقسام
    }
}
