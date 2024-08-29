﻿using Nidaa_Fidaa.Core.Dtos.Product;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Services.Abstract
{
    public interface IProductService
    {
        public Task<Product> AddProductAsync(AddProductDto addProductDto);
        public Task<Product> UpdateProductAsync(UpdateProductDto updateproductDto);
        public  Task<ProductSize> UpdateProductSizeAsync(ProductSizeDto updateProductSizeDto);
        public Task<ProductAddition> UpdateProductAdditionAsync(UpdateProductAdditionDto updateProductAdditionDto);
        public Task<ProductAddition> AddProductAdditionAsync(AddProductAdditionDto addProductAdditionDto);
        public Task<ProductSize> AddProductSizeAsync(ProductSizeDto productSizeDto);
        public Task<IReadOnlyCollection<Product>> GetProductsWithSepc(ISpecification<Product> spec);



    }
}
