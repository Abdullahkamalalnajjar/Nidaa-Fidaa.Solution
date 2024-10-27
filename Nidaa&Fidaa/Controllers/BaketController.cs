using Microsoft.AspNetCore.Mvc;
using Nidaa_Fidaa.Services.Abstract;
using Nidaa_Fidaa.Core.Dtos.Basket;
using Nidaa_Fidaa.Helpers;
using Nidaa_Fidaa.Core.Entities;

namespace Nidaa_Fidaa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService )
        {
            _basketService = basketService;
        }

  

        // 2. الحصول على السلة بواسطة معرف
        [HttpGet("get-basket-Byid")]
        public async Task<IActionResult> GetBasketById(int id)
        {
            var basket = await _basketService.GetBasketByIdAsync(id);
            if (basket == null)
                return NotFound();

            return Ok(basket);
        }

        // 3. إضافة عنصر إلى السلة
        [HttpPost("add-item")]
        public async Task<IActionResult> AddItemToBasket([FromQuery] BasketItemDto basketItemDto)
        {

            var basketItem = await _basketService.AddItemToBasketAsync(basketItemDto);
            return Ok(new ApiResponse<Basket>(200, "تم أضافة المنتج إلي السلة", basketItem));
        }

        [HttpDelete("remove-item")]
        public async Task<IActionResult> RemoveItemFromBasketAsync([FromQuery]int itemId)
        {
            var success = await _basketService.RemoveItemFromBasketAsync(itemId);

            if ( success )
            {
                return Ok(new { status= 200, message = "Item removed successfully." , });
            }

            return NotFound(new { status = 404, message = "Item not found." });
        }


        [HttpPut("edit-item")]
        public async Task<ActionResult<ApiResponse<BasketItem>>> EditItemInBasket([FromQuery]int basketItemId, [FromQuery] BasketItemDto basketItemDto)
        {
            try
            {
                var updatedItem = await _basketService.EditItemInBasketAsync(basketItemId, basketItemDto);

                if ( updatedItem==null )
                {
                    return NotFound(new ApiResponse<BasketItem>(404, "Basket item not found"));
                }

                return Ok(new ApiResponse<BasketItem>(200, "Basket item updated successfully", updatedItem));
            }
            catch ( Exception ex )
            {
                return BadRequest(new ApiResponse<BasketItem>(400, ex.Message));
            }
        }
    
    }
}
