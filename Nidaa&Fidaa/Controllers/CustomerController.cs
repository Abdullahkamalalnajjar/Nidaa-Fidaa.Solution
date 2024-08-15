using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Repository;
using Nidaa_Fidaa.Dtos;
using System.Collections.Generic;

namespace Nidaa_Fidaa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IGenericRepository<Customer> customerRepo;
        private readonly IMapper mapper;

        public CustomerController(IGenericRepository<Customer> customerRepo,IMapper mapper)
        {
            this.customerRepo = customerRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<CustomerDto>>> GetAll()
        {
           var customers = await customerRepo.GetAllAsync();
            var customersMapper = mapper.Map<IReadOnlyCollection<CustomerDto >>(customers);
            return Ok(customers);
        }
         
       [HttpPost]
     public async Task<ActionResult<string>> AddCustomer([FromQuery]Customer Newcustomer) {

            var customers = await customerRepo.AddCustomerAsync(Newcustomer);
            if (customers == "Successfully") {
                return $"Customer: {Newcustomer.Name} Add Successfully";
            }
            return "Exist Error";
        }


        [HttpGet("id")]
        public async Task<ActionResult<CustomerDto>> GetCustomerByid(int id)
        {

            var customer = await customerRepo.GetByIdAsync(id);
            if (customer==null)
            {
                return NotFound("Customer not found");
            }
            var customersMapper = mapper.Map<CustomerDto>(customer);

            return Ok(customersMapper);
        }
    }
}
