using Microsoft.AspNetCore.Mvc;
using Nidaa_Fidaa.Helpers;
using Nidaa_Fidaa.Core.Entities; // Assuming this is the namespace for your entities
using System.Threading.Tasks;
using Nidaa_Fidaa.Core.Dtos;
using Nidaa_Fidaa.Services.Abstract;
using Nidaa_Fidaa.Services.Implmentaion;
using Nidaa_Fidaa.Core.Specification.Handller;
using Nidaa_Fidaa.Core.Dtos.Shop;
using Twilio.TwiML.Voice;

namespace Nidaa_Fidaa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopservice;

        public ShopController(IShopService shopservice)
        {
          _shopservice = shopservice;
        }
        [HttpPost("AddShop")]
        public async Task<ActionResult<ApiResponse<Shop>>> AddShop([FromForm] ShopDto shopDto)
        {

            var shop = await _shopservice.AddShop(shopDto);

            if (shop != null)
            {
                var response = new ApiResponse<Shop>(200, "تم إضافة المحل  بنجاح", shop);
                return Ok(response);
            }

            return BadRequest(new ApiResponse<Shop>(400, " المحل موجود بالفعل"));


        }


        [HttpPut("update-shop")]
        public async Task<IActionResult> UpdateShopAsync([FromForm] UpdateShopDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedShop = await _shopservice.UpdateShop(dto);
            var response = new ApiResponse<Shop>(200, "تم التعديل بنجاح  ", updatedShop);

            return Ok(response);
        }


        [HttpGet("GetShops")]
        public async Task<ActionResult<List<ShopViewDto>>> GetShops()
        {
           
            var shop = await _shopservice.GetShops();
            return Ok(shop);
        }


        [HttpGet("GetShopByTraderId")]
        public async Task<ActionResult<List<ShopViewDto>>> GetShopByTraderId (int id) 
        {

            var shop = await _shopservice.GetShopsByCustomerId(id);
            if (shop != null)
            {
                return shop;
            }
                  return NotFound(new ApiResponse<string>(
                    statusCode: StatusCodes.Status404NotFound,
                    message: "المحل غير موجود"
                ));
        }

        [HttpGet("GetShopById")]
        public async Task<ActionResult<ShopViewByid>> GetShopById (int id) 
        {

            var shop =  await _shopservice.GetShopByid(id);
            if (shop != null)
            {
                return Ok(shop);
            }
                  return NotFound(new ApiResponse<string>(
                    statusCode: StatusCodes.Status404NotFound,
                    message: "المحل غير موجود"
                ));
        } 

        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> DeleteShopByid(int id)
        {

            var respone = await _shopservice.DeleteShop(id);
            return Ok(respone);
        }

        [HttpPost("add-category")]
        public async Task<ActionResult<Category>> AddCategoryAsync([FromQuery]CategoryDto categoryDto)
        {
            var category = await _shopservice.AddCategoryAsync(categoryDto);
            if (category != null)
            {
                return Ok(category);
            }
            return BadRequest(new ApiResponse<Shop>(400, "القسم موجود من قبل "));
        }


        [HttpGet("get-category")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
        {
            var categories = await _shopservice.GetCategories();
            return Ok(categories);
        }

        [HttpGet("get-product-byCategoryId")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductByCategoryId(int categoryId)
        {
            var products = await _shopservice.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }
    }


}
