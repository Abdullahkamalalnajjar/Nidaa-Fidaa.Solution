using AutoMapper;
using Nidaa_Fidaa.Core.Dtos;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Repository;
using Nidaa_Fidaa.Core.Specification;
using Nidaa_Fidaa.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Services.Implmentaion
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepo;
        private readonly IMapper mapper;

        public CustomerService(IGenericRepository<Customer> customerRepo, IMapper mapper)
        {
          _customerRepo = customerRepo;
            this.mapper = mapper;
        }
        public async Task<Customer> AddCustomerAsync(CustomerDto customer)
        {
            var customerMapping = mapper.Map<Customer>(customer);   

            await _customerRepo.AddAsync(customerMapping);
            return customerMapping;
        }

        public async Task<IReadOnlyCollection<Customer>> GetCustomerWithSpecAsync(ISpecification<Customer> specification)
        {
           var customers = await _customerRepo.GetAllWithSpecAsync(specification);
            return customers;
        }
    }
}
