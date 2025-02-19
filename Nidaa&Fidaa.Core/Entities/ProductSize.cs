﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Core.Entities
{
    public class ProductSize
    {

        public int Id { get; set; }
        public string? Size { get; set; }
        public decimal Price { get; set; }


        public int ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }

        [JsonIgnore]

        public BasketItem BasketItem { get; set; }
    }
}
