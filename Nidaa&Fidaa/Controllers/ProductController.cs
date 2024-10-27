using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Dtos.Product;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Specification.Handller;
using Nidaa_Fidaa.Helpers;
using Nidaa_Fidaa.Services.Abstract;
using System.Collections.Generic;
using System.Drawing;

namespace Nidaa_Fidaa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("addProduct")]
        public async Task<ActionResult> AddProduct([FromForm] AddProductDto productCreateDto)
        {
            var product = await _productService.AddProductAsync(productCreateDto);
          
            if (product is null)
            {
                return BadRequest(new ApiResponse<Product>(400, "المنتج موجود بالفعل"));

            }
            var response = new ApiResponse<Product>(200, "تم أضافه المنتج بنجاح", product);
            return Ok(response);
        }


        [HttpPost("add-product-addition")]
        public async Task<IActionResult> AddProductAddition([FromForm]AddProductAdditionDto addProductAdditionDto)
        {
            
            var productAddition = await _productService.AddProductAdditionAsync(addProductAdditionDto);

           
            if (productAddition == null)
            {
                return BadRequest(new ApiResponse<Product>(400, "المنتج موجود بالفعل"));
            }

            var response = new ApiResponse<ProductAddition>(200, "تم أضافه المنتج بنجاح", productAddition);


            return Ok(response);
        } 

        [HttpPost("add-product-size")]
        public async Task<IActionResult> AddProductSize([FromForm]ProductSizeDto productSizeDto)
        {
            
            var productSize = await _productService.AddProductSizeAsync(productSizeDto);

           
            if (productSize == null)
            {
                return BadRequest(new ApiResponse<Product>(400, "الحجم موجود بالفعل"));
            }

            var response = new ApiResponse<ProductSize>(200, "تم أضافه الحجم بنجاح", productSize);

            return Ok(response);
        }

        [HttpGet("GetProdcuts")]
        public async Task<ActionResult<IReadOnlyCollection<Product>>> GetProducts()
        {
            var spec = new ProductSpecification();
            var products = await _productService.GetProductsWithSepc(spec);
            return Ok(products);
        }

        [HttpGet("get-product-byId")]
        public async Task<ActionResult<ProductViewDtoByid>> GetProduct(int id)
        {
            var spec = new ProductSpecification(id);
            var products = await _productService.GetProductByIdWithSpec(spec);
            return Ok(products);
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductDto updateProductDto)
        {
           

            var updatedProduct = await _productService.UpdateProductAsync(updateProductDto);

            if (updatedProduct == null)
            {
                return NotFound("Product not found.");
            }

            var response = new ApiResponse<Product>(200, "تم التعديل علي المنتج بنجاح", updatedProduct);

            return Ok(response);
        }

        [HttpPut("update-size")]
        public async Task<IActionResult> UpdateProductSize([FromQuery] UpdateProductSizeDto updateProductSizeDto)
        {
            if (updateProductSizeDto == null)
            {
                return BadRequest("Product size data is null.");
            }

            var updatedProductSize = await _productService.UpdateProductSizeAsync(updateProductSizeDto);

            if (updatedProductSize == null)
            {
                return NotFound("Product size not found.");
            }

            var response = new ApiResponse<ProductSize>(200, "تم التعديل علي  حجم المنتج بنجاح", updatedProductSize);

            return Ok(response);
        }

        [HttpPut("update-addition")]
        public async Task<IActionResult> UpdateProductAddition([FromQuery] UpdateProductAdditionDto updateProductAdditionDto)
        {
            if (updateProductAdditionDto == null)
            {
                return BadRequest("Product addition data is null.");
            }

            var updatedProductAddition = await _productService.UpdateProductAdditionAsync(updateProductAdditionDto);

            if (updatedProductAddition == null)
            {
                return NotFound("Product addition not found.");
            }

            var response = new ApiResponse<ProductAddition>(200, "تم التعديل بنجاح", updatedProductAddition);

            return Ok(response);
        }
        [HttpDelete("delete-productaddition")]

        public async Task<IActionResult> DeleteProductAddition(int id)
        {


            var productaddition = await _productService.DeleteProductAdditionAsync(id);

            if (productaddition == false) {
                return BadRequest(new ApiResponse<ProductAddition>(200, " حدث خطا"));
            }
            var response = new ApiResponse<ProductAddition>(200, "تم الحذف بنجاح");

            return Ok(response);
        }

        [HttpDelete("delete-product-size")]

        public async Task<IActionResult> DeleteProductSize(int id)
        {


            var productsize = await _productService.DeleteProductSizeAsync(id);

            if (productsize == false)
            {
                return BadRequest(new ApiResponse<ProductSize>(200, " حدث خطا"));
            }
            var response = new ApiResponse<ProductSize>(200, "تم الحذف بنجاح");

            return Ok(response);
        }

        [HttpGet("search-product-byTitle")]

        public async Task<ActionResult<Product>> SearchProductByTitle(string title) { 
        
        
            var spec= new ProductSpecification(title);
            var product = await _productService.SearchProductAsync(spec);

            if (product == null)
            {
                return NotFound(new ApiResponse<Product>(404,"لا يوجد نتائج"));
            }
            return Ok(product);
        }

        [HttpGet("price-range")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsInPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            var products = await _productService.GetProductsInPriceRangeAsync(minPrice, maxPrice);

            if (products == null)
            {
                return NotFound(new ApiResponse< IEnumerable < Product >> (404, "لا يوجد نتائج"));
            }
            return Ok(products);
        }

    }
}
