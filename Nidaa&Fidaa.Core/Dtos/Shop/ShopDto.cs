﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos.Shop
{
    public class ShopDto
    {
      //  public IFormFile MerchantPhotoUrl { get; set; }
        public IFormFile BaseShopPhotoUrl { get; set; }
        public IFormFile PhotoUrl { get; set; }
        public string Location { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public int TraderId { get; set; }
        public List<int> SelectCatergoryIds { get; set; }  // قائمة معرفات الأقسام
        public decimal Rating { get; set; }
        public decimal DeliveryTime { get; set; }
        public decimal DeliveryPrice { get; set; }
    }


}
