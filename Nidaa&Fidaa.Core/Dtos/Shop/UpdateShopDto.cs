using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos.Shop
{
    public class UpdateShopDto
    {
        public int Id { get; set; }
        public IFormFile MerchantPhotoUrl { get; set; }
        public IFormFile ShopPhotoUrl { get; set; }
        public string Location { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public int CustomerId { get; set; }

        public List<int> SelectCatergoryIds { get; set; }  // قائمة معرفات الأقسام

    }
}
