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

        public Task<Basket> GetBasketByIdAsync(int id);
        public Task<BasketItem> AddItemToBasketAsync(BasketItemDto basketItemDto);
        public Task<BasketItem> RemoveItemFromBasketAsync( int productId);

    }
}
