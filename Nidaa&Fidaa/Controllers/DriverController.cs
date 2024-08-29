using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nidaa_Fidaa.Core.Dtos.Driver;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Repository;
using Nidaa_Fidaa.Core.Specification.Handller;
using Nidaa_Fidaa.Helpers;
using Nidaa_Fidaa.Respository.Data.Configurations;
using Nidaa_Fidaa.Services.Abstract;
using Nidaa_Fidaa.Services.Implmentaion;

namespace Nidaa_Fidaa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService driverService;

        public DriverController(IDriverService driverService) {
            this.driverService = driverService;
        }

        [HttpPost("AddDriver")]
        public async Task<ActionResult<ApiResponse<Driver>>> AddDriver ([FromForm] AddDriverDto Newdriver)
        {
            var driver = await driverService.AddDriver(Newdriver);
            if (driver!=null)
            {
                var response = new ApiResponse<Driver>(200, "تم أضافه السائق بنجاح",driver);
              
                return Ok(response);
            }

            return BadRequest(new ApiResponse<Driver>(400, "السائق موجود بفعل"));

        }

        [HttpGet("GetDriver")]
        public async Task<ActionResult<IReadOnlyList<Driver>>> GetDrivers()
        {
            var spec =  new DriverSpecification();
        var drivers= await driverService.GetDriversWithSepc(spec);
            return Ok(drivers);
        }


    }
}
