using Microsoft.AspNetCore.Mvc;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Helpers;
using Nidaa_Fidaa.Services.Abstract;
using System.Text.Json;
using System.Text.Json.Serialization;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    public static class JsonSerializerConfig
    {
        public static JsonSerializerOptions ConfigureOptions()
        {
            return new JsonSerializerOptions
            {
                ReferenceHandler=System.Text.Json.Serialization.ReferenceHandler.Preserve,
                WriteIndented=true // Optional: for better readability
            };
        }
    }

    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService=orderService;
    }

    [HttpPost("create-order")]
    public async Task<ActionResult<ApiResponse<Order>>> CreateOrderFromBasket(int customerId, string? location  )
    {
         var order = await _orderService.CreateOrderAsync(customerId,location);
        if (order == null)
        {
            return BadRequest(new ApiResponse<Order>(400,"الزبون مش موجود"));
        }
        var response = new ApiResponse<Order>(200, "اتفضل يامعلم الأوردر", order);
            return Ok(response);
        
  
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);

        //var options = new JsonSerializerOptions
        //{
        //    DefaultIgnoreCondition=JsonIgnoreCondition.WhenWritingNull,
        //    ReferenceHandler=ReferenceHandler.Preserve,
        //    WriteIndented=true
        //};

        //var json = JsonSerializer.Serialize(order, options);

     //   return Content(json, "application/json");
        var response = new ApiResponse<Order>(200, "اتفضل يامعلم الأوردر أهو", order);
        return Ok(response);
    }

    [HttpGet("get-orders/{customerId}")]
    public async Task<ActionResult<ApiResponse<List<Order>>>> GetOrdersByCustomerId(int customerId)
    {
        var orders = await _orderService.GetOrderByCostomerIdAsync(customerId);

        if ( orders==null||!orders.Any() )
        {
            return NotFound(new ApiResponse<List<Order>>(404, "لا توجد طلبات لهذا العميل"));
        }

        return Ok(new ApiResponse<List<Order>>(200, "تم جلب الطلبات بنجاح", orders));
    }
}
