using AutoMapper;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Dtos.Basket;
using System.Threading.Tasks;
using Nidaa_Fidaa.Core.Repository;
using Nidaa_Fidaa.Services.Abstract;
using Nidaa_Fidaa.Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace Nidaa_Fidaa.Services.Implmentaion
{
    public class BasketService : IBasketService
    {
        private readonly IGenericRepository<Basket> _basketRepository;
        private readonly IGenericRepository<BasketItem> _basketItemRepository;
        private readonly IGenericRepository<ProductAddition> _productAdditionRepository;
        private readonly IMapper _mapper;

        public BasketService(IGenericRepository<Basket> basketRepository,
                             IGenericRepository<BasketItem> basketItemRepository,
                             IGenericRepository<ProductAddition> productAdditionRepository,
                             IMapper mapper)
        {
            _basketRepository=basketRepository;
            _basketItemRepository=basketItemRepository;
            _productAdditionRepository=productAdditionRepository;
            _mapper=mapper;
        }

        public async Task<Basket> AddBasketAsync(BasketDto basketDto)
        {
            var basket = _mapper.Map<Basket>(basketDto);

            await _basketRepository.AddAsync(basket);

            return basket;
        }

        public async Task<Basket> AddItemToBasketAsync(BasketItemDto basketItemDto)
        {
            // تحقق من وجود سلة بها معرف السلة المحدد
            var basket = await _basketRepository.GetByIdAsync(basketItemDto.BasketId);
            if ( basket==null )
            {
                throw new Exception("Basket not found.");
            }

            // لا حاجة للتحقق من التكرار في هذه الحالة
            // قم بإنشاء عنصر سلة جديد
            var basketItem = new BasketItem
            {
                BasketId=basketItemDto.BasketId,
                ProductId=basketItemDto.ProductId,
                ProductSizeId=basketItemDto.ProductSizeId,
                Quantity=basketItemDto.Quantity,
                //   UnitPrice=basketItemDto.UnitPrice,
                //    TotalPrice=basketItemDto.TotalPrice,
                Note=basketItemDto.Note,
            };

            // إضافة الإضافات إذا كانت موجودة
            if ( basketItemDto.AdditionIds!=null )
            {
                foreach ( var additionId in basketItemDto.AdditionIds )
                {
                    var addition = await _productAdditionRepository.GetByIdAsync(additionId);
                    if ( addition!=null )
                    {
                        basketItem.Additions.Add(addition);
                    }
                }
            }

            // إضافة العنصر إلى السلة
            basket.Items.Add(basketItem);

            // تحديث السلة في قاعدة البيانات
            await _basketRepository.UpdateAsync(basket);

            return basket;
        }

        public async Task<BasketItem> EditItemInBasketAsync(int basketItemId, BasketItemDto basketItemDto)
        {
            // البحث عن العنصر في السلة باستخدام معرفه
            var basketItem = await _basketItemRepository.GetByIdAsync(basketItemId);
            if ( basketItem==null )
            {
                throw new Exception("Basket item not found.");
            }

            // تعديل الحقول المطلوبة
            basketItem.ProductId=basketItemDto.ProductId!=0 ? basketItemDto.ProductId : basketItem.ProductId;
            basketItem.ProductSizeId=basketItemDto.ProductSizeId!=0 ? basketItemDto.ProductSizeId : basketItem.ProductSizeId;
            basketItem.Quantity=basketItemDto.Quantity!=0 ? basketItemDto.Quantity : basketItem.Quantity;
            // basketItem.UnitPrice=basketItemDto.UnitPrice!=0 ? basketItemDto.UnitPrice : basketItem.UnitPrice;
            //  basketItem.TotalPrice=basketItemDto.TotalPrice!=0 ? basketItemDto.TotalPrice : basketItem.TotalPrice;
            basketItem.Note=!string.IsNullOrEmpty(basketItemDto.Note) ? basketItemDto.Note : basketItem.Note;

            // تعديل الإضافات إذا كانت موجودة
            if ( basketItemDto.AdditionIds!=null )
            {
                // حذف الإضافات القديمة
                basketItem.Additions.Clear();

                // إضافة الإضافات الجديدة
                foreach ( var additionId in basketItemDto.AdditionIds )
                {
                    var addition = await _productAdditionRepository.GetByIdAsync(additionId);
                    if ( addition!=null )
                    {
                        basketItem.Additions.Add(addition);
                    }
                }
            }

            // حفظ التغييرات في قاعدة البيانات
            await _basketItemRepository.UpdateAsync(basketItem);

            return basketItem;
        }


        public async Task<Basket?> GetBasketByCustomerIdAsync(int customerId)
        {
            return await _basketRepository.GetTableNoTracking()
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.CustomerId==customerId);
        }


        public async Task<Basket> GetBasketByIdAsync(int id)
        {
            var basket = await _basketRepository.GetTableNoTracking()
                .Include(ad => ad.Items).ThenInclude(ad => ad.Additions)
                .Include(ad => ad.Items).ThenInclude(ad => ad.ProductSize)
       .Include(b => b.Items) // تضمين عناصر السلة
           .ThenInclude(bi => bi.Product) // تضمين المنتج المرتبط بكل عنصر
               .ThenInclude(p => p.ProductSizes) // تضمين أحجام المنتجات
           .Include(b => b.Items) // تضمين نفس عناصر السلة مرة أخرى
               .ThenInclude(bi => bi.Product) // تضمين المنتج مرة أخرى
               .ThenInclude(p => p.ProductAdditions) // تضمين الإضافات المرتبطة بالمنتج
       .FirstOrDefaultAsync(b => b.Id==id); // البحث عن السلة بناءً على المعرف
            return basket;
        }


        public async Task<IReadOnlyCollection<Basket>> GetBasketsAsync(ISpecification<Basket> specification)
        {
            var baskets = await _basketRepository.GetAllWithSpecAsync(specification);
            return baskets;
        }


        public async Task<bool> RemoveItemFromBasketAsync(int itemId)
        {
            var basketItem = await _basketItemRepository.GetByIdAsync(itemId);

            if ( basketItem==null )
            {
                return false; // Item not found
            }

            // Remove related ProductAdditions
            foreach ( var addition in basketItem.Additions.ToList() )
            {
               await _productAdditionRepository.DeleteAsync(addition);
            }

            // Now remove the BasketItem
           await _basketItemRepository.DeleteAsync(basketItem);

            return true; // Item successfully removed
        }

    }

}
