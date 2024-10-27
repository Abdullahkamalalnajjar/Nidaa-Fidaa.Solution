using Microsoft.AspNetCore.Mvc;
using Nidaa_Fidaa.Core.Dtos.Shop;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Helpers;
using Nidaa_Fidaa.Services.Abstract;
using Nidaa_Fidaa.Services.Implmentaion;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavouritesController : ControllerBase
    {
        private readonly IFavouriteService _favouriteService;

        public FavouritesController(IFavouriteService favouriteService)
        {
            _favouriteService = favouriteService;
        }

        [HttpPost("toggle-product-favourite")]
        public async Task<ActionResult<ApiResponse<ProductFavourite>>> AddOrRemoveProductFromFavouriteAsync(int customerId, int productId)
        {
           

                var favourite = await _favouriteService.AddOrRemoveProductFromFavouriteAsync(customerId, productId);
                var response = new ApiResponse<ProductFavourite>(200, "تم أضافه المنتج الي المفضلة", favourite);
            if (favourite != null) 
                return Ok(response);
            else 
                return Ok(new ApiResponse<ProductFavourite>(200, " تم ازالة المنتج من المفضلة "));
            
        }
        [HttpPost("toggle-shop-favourite")]
        public async Task<ActionResult<ApiResponse<ShopFavourite>>> ToggleShopFavourite([FromQuery] int customerId, [FromQuery] int shopId)
        {
            var favourite = await _favouriteService.AddOrRemoveShopFromFavouriteAsync(customerId, shopId);
            if (favourite == null)
            {
                return Ok(new ApiResponse<ShopFavourite>(200,"تم ازالة المحل من المفضلة "));
            }
            var response = new ApiResponse<ShopFavourite>(200, "تم أضافه المحل الي المفضلة", favourite);

            return Ok(response);
        }



        [HttpGet("get-favourites")]
        public async Task<IActionResult> GetFavourites()
        {
            var favourites = await _favouriteService.GetFavouriteAsync();
            return Ok(favourites);
        }

        [HttpGet("get-product-favourites-OfCustomer")]

        public async Task<ActionResult<IReadOnlyCollection<Product>>> GetProductFavouritesByCustomerId(int id)
        {
            var product = await _favouriteService.GetProductFavouritesByCustomerIdAsync(id);
            return Ok(product);
        }
        [HttpGet("get-shop-favourites-OfCustomer")]

        public async Task<IActionResult> GetShopFavouritesByCustomerId(int id)
        {
            var shops = await _favouriteService.GetShopFavouritesByCustomerIdAsync(id);
            return Ok(shops);
        }
        [HttpGet("get-product-BycustomerId")]
        public async Task<IActionResult> GetProducts(int customerId)
        {
            var products = await _favouriteService.GetProductsWithFavoritesAsync(customerId);
            return Ok(products);
        }

        [HttpGet("get-shop-BycustomerId")]
        public async Task<IActionResult> GetShops(int customerId)
        {
            var shops = await _favouriteService.GetShopsWithFavoritesAsync(customerId);
            return Ok(shops);
        }


    }
}
