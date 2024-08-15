using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Repository;

namespace Nidaa_Fidaa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeActivityController : ControllerBase
    {
        private readonly IGenericRepository<TradeActivity> tradeRepo;

        public TradeActivityController(IGenericRepository<TradeActivity> tradeRepo)
        {
            this.tradeRepo = tradeRepo;
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddCustomer([FromQuery] TradeActivity NewTradeActivity)
        {

            var tradeActity = await tradeRepo.AddCustomerAsync(NewTradeActivity);
            if (tradeActity == "Successfully")
            {
                return $"Customer: {NewTradeActivity.Name} Add Successfully";
            }
            return "Exist Error";
        }
    }
}
