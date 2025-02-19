﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Nidaa_Fidaa.Core.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string? BasePicture { get; set; }

        public decimal Rating { get; set; } = 0.0m; 
        public decimal DeliveryTime { get; set; }  
        public decimal DeliveryPrice { get; set; } = 0.0m; 
        public int CategoryId { get; set; }
                                                          
        [JsonIgnore]
        public Category Category { get; set; }
        public ICollection<Image> Images { get; set; }= new List<Image>();

        public List<ProductSize> ProductSizes { get; set; } = new List<ProductSize> ();

        public List<ProductAddition> ProductAdditions { get; set; } = new List<ProductAddition> ();


        public int ShopId { get; set; }
        [JsonIgnore]
        public Shop Shop { get; set; }
        public ICollection<ProductFavourite> ProductFavourites { get; set; } = new List<ProductFavourite> ();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem> ();



    }

}
