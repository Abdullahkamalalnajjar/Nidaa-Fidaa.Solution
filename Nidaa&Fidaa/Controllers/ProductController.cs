using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Dtos.Product;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Specification.Handller;
using Nidaa_Fidaa.Helpers;
using Nidaa_Fidaa.Services.Abstract;
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

            
            return Ok(productAddition);
        } 

        [HttpPost("add-product-size")]
        public async Task<IActionResult> AddProductSize([FromForm]ProductSizeDto productSizeDto)
        {
            
            var productSize = await _productService.AddProductSizeAsync(productSizeDto);

           
            if (productSize == null)
            {
                return BadRequest(new ApiResponse<Product>(400, "الحجم موجود بالفعل"));
            }

            
            return Ok(productSize);
        }

        [HttpGet("GetProdcuts")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            var spec = new ProductSpecification();
            var products = await _productService.GetProductsWithSepc(spec);
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

            return Ok(updatedProduct);
        }

        [HttpPut("update-size")]
        public async Task<IActionResult> UpdateProductSize([FromBody] ProductSizeDto updateProductSizeDto)
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

            return Ok(updatedProductSize);
        }

        [HttpPut("update-addition")]
        public async Task<IActionResult> UpdateProductAddition([FromBody] UpdateProductAdditionDto updateProductAdditionDto)
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

            return Ok(updatedProductAddition);
        }

    }
}
