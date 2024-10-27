using Nidaa_Fidaa.Core.Dtos;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Services.Abstract
{
    public interface ICustomerService
    {
        public Task<Customer> AddCustomerAsync(CustomerDto customer);
        public Task<IReadOnlyCollection<Customer>> GetCustomerWithSpecAsync(ISpecification<Customer> specification);
        public Task<Customer> GetCustomerById(int id);
    }
}
