using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nidaa_Fidaa.Services.Abstract;

namespace Nidaa_Fidaa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly IFilterService _filterService;

        public FilterController(IFilterService filterService)
        {
            _filterService = filterService;
        }
        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] string? searchTerm=null, decimal? minPrice =null , decimal? maxPrice = null)
        {
          

           

            var result = await _filterService.FilterAsync(searchTerm, minPrice, maxPrice);

            return Ok(result);
        }

    }
}
