using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Repository;
using System.Collections.Generic;
using Nidaa_Fidaa.Services.Abstract;
using Nidaa_Fidaa.Helpers;
using Nidaa_Fidaa.Services.Implmentaion;
using Nidaa_Fidaa.Core.Specification;
using Nidaa_Fidaa.Core.Specification.Handller;
using Nidaa_Fidaa.Core.Dtos.Trader;

namespace Nidaa_Fidaa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraderController : ControllerBase
    {
  
private readonly ITraderService customerService;

        public TraderController(ITraderService  customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<Trader>>> GetAllTraders()
        {
            var spec = new TraderSpecification();
           var customers = await customerService.GetTradersWithSepc(spec);
            return Ok(customers);
        }

        [HttpPost("AddTrader")]
        public async Task<ActionResult<ApiResponse<Trader>>> AddTrader([FromForm] AddTrader Newcustomer) {

            //var addCustomer = await customerService.AddCustomer(Newcustomer);
            //var response = new ApiResponse<Customer>(200, "تم أضافه التاجر بنجاح ", addCustomer);

            //return Ok(response);
            var addCustomer = await customerService.AddTrader(Newcustomer);
            if (addCustomer != null)
            {
                var response = new ApiResponse<Trader>(200, "تم أضافه التاجر بنجاح", addCustomer);

                return Ok(response);
            }

            return BadRequest(new ApiResponse<Driver>(400, "التاجر موجود بفعل"));
        }


        [HttpGet("id")]
        public async Task<ActionResult<Trader>> GetTraderByid(int id)
        {
            var spec = new TraderSpecification(id);      
            var customer = await customerService.GetTraderByidWithSpec(spec);
            if (customer == null) return NotFound(new ApiResponse<Driver>(404, "التاجر مش موجود"));
            return Ok(customer);
        }

    

        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> DeleteCustomerByid(int id)
        {

           var customer= await customerService.DeleteTraderByid(id);
          return Ok(customer);
        }

        [HttpGet("GetCities")]
        public async Task<ActionResult<ApiResponse<IEnumerable<string>>>> GetCities()
        {
            var cities = await customerService.GetCitiesAsync();

            var response = new ApiResponse<IEnumerable<string>>(
                statusCode: StatusCodes.Status200OK,
                message: "تم الحصول على قائمة المدن بنجاح",
                data: cities
            );

            return Ok(response);
        }


        [HttpGet("GetZonesByCity")]
        public async Task<ActionResult<ApiResponse<IEnumerable<string>>>> GetZonesByCity(string city)
        {
            var zones = await customerService.GetZonesByCityAsync(city);

            if (zones == null || !zones.Any())
            {
                return BadRequest(new ApiResponse<IEnumerable<string>>(
                    statusCode: StatusCodes.Status400BadRequest,
                    message: "المدينة غير موجودة"
                ));
            }

            var response = new ApiResponse<IEnumerable<string>>(
                statusCode: StatusCodes.Status200OK,
                message: "تم الحصول على قائمة المناطق بنجاح",
                data: zones
            );

            return Ok(response);
        }

        [HttpGet("SearchGovernorates")]
        public async Task<ActionResult<ApiResponse<IEnumerable<string>>>> SearchGovernorates(string query)
        {
            var result = await customerService.SearchGovernoratesAsync(query);
            if (result!=null)
            {
                var response = new ApiResponse<IEnumerable<string>>(
               statusCode: StatusCodes.Status200OK,
               message: "تم العثور على النتائج",
               data: result
           );

                return Ok(response);
            }
            else if (result == null || !result.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<string>>(
                    statusCode: StatusCodes.Status404NotFound,
                    message: "لا توجد نتائج"
                ));
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("SearchZonesByCity")]
        public async Task<ActionResult<ApiResponse<IEnumerable<string>>>> SearchZonesByCity(string city, string query)
        {
            var result = await customerService.SearchZonesByCityAsync(city, query);

            if (result == null || !result.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<string>>(
                    statusCode: StatusCodes.Status404NotFound,
                    message: "لا توجد نتائج"
                ));
            }

            var response = new ApiResponse<IEnumerable<string>>(
                statusCode: StatusCodes.Status200OK,
                message: "تم العثور على النتائج",
                data: result
            );

            return Ok(response);
        }



    }
}
