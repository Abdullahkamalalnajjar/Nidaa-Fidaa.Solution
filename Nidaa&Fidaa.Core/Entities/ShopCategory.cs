﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Nidaa_Fidaa.Core.Entities
{
    public class ShopCategory
    {
        public int ShopId { get; set; }
        [JsonIgnore]
        public Shop Shop { get; set; }

        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
    }

}
