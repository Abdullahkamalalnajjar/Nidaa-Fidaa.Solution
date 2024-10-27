using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nidaa_Fidaa.Core.Dtos;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Specification.Handller;
using Nidaa_Fidaa.Helpers;
using Nidaa_Fidaa.Services.Abstract;
using Nidaa_Fidaa.Services.Implmentaion;

namespace Nidaa_Fidaa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("add-customer")]

        public async Task<ActionResult<ApiResponse<Customer>>> AddCustomerAsync([FromForm] CustomerDto customerDto)
        {

            var customer = await _customerService.AddCustomerAsync(customerDto);

            var response = new ApiResponse<Customer>(200, "تم أضافه الزبون بنجاح", customer);
            return Ok(response);
        }

        [HttpGet("GetCustomer")]
        public async Task<ActionResult<IReadOnlyList<Customer>>> GetCustomers()
        {
            var spec = new CustomerSpecification();
            var customers = await _customerService.GetCustomerWithSpecAsync(spec);
            return Ok(customers);
        }
        [HttpGet("get-customer-byId")]
        public async Task<ActionResult<ApiResponse<Customer>>> GetCustomerbyId([FromQuery] int id) { 
            var customer= await _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound( new ApiResponse<Customer>(404, "غير موجود "));
            }
            else
            {
                var response = new ApiResponse<Customer>(200, $"موجود {customer.Name} ", customer);
                return Ok(response);

            }
        }
    }
}
