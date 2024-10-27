using Nidaa_Fidaa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Services.Abstract
{
    public interface IOrderService
    {
        public Task<Order> CreateOrderAsync(int customerId, string? location);
        public Task<List<Order>> GetOrderByCostomerIdAsync(int customerId);
        public Task<Order?> GetOrderByIdAsync(int? id);   
    }
}
