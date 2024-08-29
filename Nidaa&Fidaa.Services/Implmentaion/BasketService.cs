using AutoMapper;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Dtos.Basket;
using System.Threading.Tasks;
using Nidaa_Fidaa.Core.Repository;
using Nidaa_Fidaa.Services.Abstract;
using Nidaa_Fidaa.Core.Specification;

namespace Nidaa_Fidaa.Services.Implmentaion
{
    public class BasketService : IBasketService
    {
        private readonly IGenericRepository<Basket> _basketRepository;
        private readonly IGenericRepository<BasketItem> _basketItemRepository;
        private readonly IMapper _mapper;

        public BasketService(IGenericRepository<Basket> basketRepository,
                             IGenericRepository<BasketItem> basketItemRepository,
                             IMapper mapper)
        {
            _basketRepository = basketRepository;
            _basketItemRepository = basketItemRepository;
            _mapper = mapper;
        }

        public async Task<Basket> AddBasketAsync(BasketDto basketDto)
        {
            var basket =_mapper.Map<Basket>(basketDto);

            await _basketRepository.AddAsync(basket);

            return basket;
        }

        public Task<BasketItem> AddItemToBasketAsync(BasketItemDto basketItemDto)
        {
            throw new NotImplementedException();
        }

        public Task<Basket> GetBasketByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Basket>> GetBasketsAsync(ISpecification<Basket> specification)
        {
            var baskets = await _basketRepository.GetAllWithSpecAsync(specification);
            return baskets;
        }

        public Task<BasketItem> RemoveItemFromBasketAsync(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
