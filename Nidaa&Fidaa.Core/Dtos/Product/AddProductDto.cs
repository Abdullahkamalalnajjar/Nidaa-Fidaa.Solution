﻿using Microsoft.AspNetCore.Http;
using Nidaa_Fidaa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Dtos.Product
{
    public class AddProductDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public IFormFile BaseImage { get; set; }
        public int CategoryId { get; set; }
        public int ShopId { get; set; }
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();

        public decimal Rating { get; set; }
        public decimal DeliveryTime { get; set; }
        public decimal DeliveryPrice { get; set; }



    }
}
