using Nidaa_Fidaa.Core.Dtos.Basket;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Services.Abstract
{
    public interface IBasketService
    { 
        public Task<Basket> AddBasketAsync(BasketDto basketDto);
        public Task<IReadOnlyCollection<Basket>> GetBasketsAsync(ISpecification<Basket> specification);
        public Task<Basket?> GetBasketByCustomerIdAsync(int customerId);

        public Task<Basket> GetBasketByIdAsync(int id);
        public Task<Basket> AddItemToBasketAsync(BasketItemDto basketItemDto);
        public Task<bool> RemoveItemFromBasketAsync( int itemId);
        public Task<BasketItem> EditItemInBasketAsync(int basketItemId, BasketItemDto basketItemDto);

    }
}
