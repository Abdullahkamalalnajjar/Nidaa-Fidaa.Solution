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
        private readonly IGenericRepository<Basket> _basketRepository;
        private readonly IMapper mapper;

        public CustomerService(IGenericRepository<Customer> customerRepo,IGenericRepository<Basket> basketRepository, IMapper mapper)
        {
          _customerRepo = customerRepo;
            _basketRepository =basketRepository;
            this.mapper = mapper;
        }


        public async Task<Customer> AddCustomerAsync(CustomerDto customerDto)
        {
            // تحويل CustomerDto إلى Customer entity
            var customer = mapper.Map<Customer>(customerDto);

            // إضافة العميل إلى قاعدة البيانات
            await _customerRepo.AddAsync(customer);

            // إنشاء سلة جديدة للعميل
            var basket = new Basket
            {
                CustomerId = customer.Id,
                Items = new List<BasketItem>()
            };

            // إضافة السلة إلى قاعدة البيانات
            await _basketRepository.AddAsync(basket);

            return customer;
        }


        public async Task<Customer> GetCustomerById(int id)
        {
           var existingCustomer=  _customerRepo.GetTableNoTracking().Where(c=>c.Id.Equals(id)).FirstOrDefault();
            if (existingCustomer != null)
            {
                return existingCustomer;
            }
            else
            {
               return null;
            }
            
        }

        public async Task<IReadOnlyCollection<Customer>> GetCustomerWithSpecAsync(ISpecification<Customer> specification)
        {
           var customers = await _customerRepo.GetAllWithSpecAsync(specification);
            return customers;
        }
    }
}
