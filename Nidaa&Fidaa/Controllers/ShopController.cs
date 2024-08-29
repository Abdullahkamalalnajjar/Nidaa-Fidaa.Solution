using Microsoft.AspNetCore.Mvc;
using Nidaa_Fidaa.Helpers;
using Nidaa_Fidaa.Core.Entities; // Assuming this is the namespace for your entities
using System.Threading.Tasks;
using Nidaa_Fidaa.Core.Dtos;
using Nidaa_Fidaa.Services.Abstract;
using Nidaa_Fidaa.Services.Implmentaion;
using Nidaa_Fidaa.Core.Specification.Handller;
using Nidaa_Fidaa.Core.Dtos.Shop;

namespace Nidaa_Fidaa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _merchantService;

        public ShopController(IShopService merchantService)
        {
            _merchantService = merchantService;
        }
        [HttpPost("AddShop")]
        public async Task<ActionResult<ApiResponse<Shop>>> AddShop([FromForm] ShopDto merchantDto)
        {

            var merchant = await _merchantService.AddShop(merchantDto);

            if (merchant != null)
            {
                var response = new ApiResponse<Shop>(200, "تم إضافة المحل  بنجاح", merchant);
                return Ok(response);
            }

            return BadRequest(new ApiResponse<Shop>(400, " المحل موجود بالفعل"));


        }


        [HttpPut("UpdateShop")]

        public async Task<ActionResult<Shop>> UpdateShopAsync([FromForm] UpdateShopDto merchantDto)
        {
            var updateMerchant =  await _merchantService.UpdateShop(merchantDto);
            if (updateMerchant != null) {

                var response = new ApiResponse<Shop>(200, $"{updateMerchant.BusinessName} :  تم تعديل المحل  بنجاح  ", updateMerchant);
                return Ok(response);
            }
            return BadRequest(new ApiResponse<Shop>(400, " المحل  غير موجود "));

        }

        [HttpGet("GetShops")]
        public async Task<ActionResult<IReadOnlyList<Shop>>> GetShops()
        {
           
            var merchants = await _merchantService.GetShops();
            return Ok(merchants);
        }


        [HttpGet("GetShopByCustomerId")]
        public async Task<ActionResult<List<Shop>>> GetShopById (int id) 
        {

            var merchant  =await _merchantService.GetShopsByCustomerId(id);
            if (merchant != null)
            {
                return merchant;
            }
                  return NotFound(new ApiResponse<string>(
                    statusCode: StatusCodes.Status404NotFound,
                    message: "المحل غير موجود"
                ));
        } 

        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> DeleteShopByid(int id)
        {

            var respone = await _merchantService.DeleteShop(id);
            return Ok(respone);
        }

        [HttpPost("AddCategory")]
        public async Task<ActionResult<Category>> AddCategoryAsync([FromQuery]CategoryDto categoryDto)
        {
            var category = await _merchantService.AddCategoryAsync(categoryDto);
            if (category != null)
            {
                return Ok(category);
            }
            return BadRequest(new ApiResponse<Shop>(400, "القسم موجود من قبل "));
        }


        [HttpGet("GetCategories")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
        {
            var categories = await _merchantService.GetCategories();
            return Ok(categories);
        }
    }


}
