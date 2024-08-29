using Microsoft.AspNetCore.Mvc;
using Nidaa_Fidaa.Core.Dtos.Basket;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Specification.Handller;
using Nidaa_Fidaa.Services.Abstract;
using Nidaa_Fidaa.Services.Implmentaion;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost("add-basket")]
        public async Task<ActionResult<BasketDto>> AddBasket([FromForm] BasketDto basketDto)
        {
            var addedBasket = await _basketService.AddBasketAsync(basketDto);
            return Ok(addedBasket);
        }

        [HttpGet("get-baskets")]
        public async Task<ActionResult<IReadOnlyCollection<Basket>>> GetBaskets()
        {
            var spec = new BasketSpecification();
            var baskets = await _basketService.GetBasketsAsync(spec);
            return Ok(baskets);
        }
    }
}
