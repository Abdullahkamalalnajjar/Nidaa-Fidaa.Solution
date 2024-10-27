using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Dtos.Favourite;
using Nidaa_Fidaa.Core.Dtos.Shop;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Repository;
using Nidaa_Fidaa.Services.Abstract;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Services.Implmentaion
{
    public class FavouriteService : IFavouriteService
    {
        private readonly IGenericRepository<ProductFavourite> _productfavouriteRepository;
        private readonly IGenericRepository<ShopFavourite> _shopfavouriteRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Shop> _shopRepository;
        private readonly IMapper _mapper;

        public FavouriteService(
            IGenericRepository<ProductFavourite> productfavouriteRepository,
            IGenericRepository<ShopFavourite> shopfavouriteRepository,
            IGenericRepository<Customer> customerRepository,
            IGenericRepository<Product> productRepository,
            IGenericRepository<Shop> shopRepository,
            IMapper mapper
            
            )
          
        {
            _productfavouriteRepository = productfavouriteRepository;
           _shopfavouriteRepository = shopfavouriteRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _shopRepository = shopRepository;
           _mapper = mapper;
        }

        public async Task<ProductFavourite> AddOrRemoveProductFromFavouriteAsync(int customerId, int productId)
        {
            var existingFavourite = await _productfavouriteRepository.GetTableNoTracking()
                .FirstOrDefaultAsync(f => f.CustomerId == customerId && f.ProductId == productId);

            if (existingFavourite != null)
            {
                await _productfavouriteRepository.DeleteAsync(existingFavourite);
                return null; //    تم حذفه من المفضلة
            }
            else
            {
                var favourite = new ProductFavourite
                {
                    CustomerId = customerId,
                    ProductId = productId,
                    IsFavourite = true
                };

                await _productfavouriteRepository.AddAsync(favourite);
                return favourite; 
            }
        }

        public async Task<ShopFavourite> AddOrRemoveShopFromFavouriteAsync(int customerId, int shopId)
        {
            // ابحث عن المفضل الحالي للمتجر
            var existingFavourite = await _shopfavouriteRepository.GetTableNoTracking()
                .FirstOrDefaultAsync(f => f.CustomerId == customerId && f.ShopId == shopId);

            // إذا كان موجودًا في المفضلة، احذفه (إزالة من المفضلة)
            if (existingFavourite != null)
            {
                await _shopfavouriteRepository.DeleteAsync(existingFavourite);
                return null; // تعني أن المتجر تم حذفه من المفضلة
            }
            else
            {
                // إذا لم يكن موجودًا، أضفه إلى المفضلة
                var favourite = new ShopFavourite
                {
                    CustomerId = customerId,
                    ShopId = shopId,
                    IsFavourite = true
                };

                await _shopfavouriteRepository.AddAsync(favourite);
                return favourite; // تعني أن المتجر تم إضافته إلى المفضلة
            }
        }
        public async Task<IReadOnlyCollection<ProductFavourite>> GetFavouriteAsync()
        {
            return await _productfavouriteRepository.GetAllAsync();
        }

      
        public async Task<IReadOnlyCollection<Product>> GetProductFavouritesByCustomerIdAsync(int customerId)
        {
            var products = await _productfavouriteRepository.GetTableNoTracking()
              .Where(f => f.CustomerId == customerId && f.IsFavourite == true)
              .Select(f => f.Product)
              .ToListAsync();

            return products;


        }
  



        public async Task<IEnumerable<ProductWithFavoriteDto>> GetProductsWithFavoritesAsync(int customerId)
        {
            return await _productRepository.GetTableNoTracking()
                .Select(p => new ProductWithFavoriteDto
                {
                    ProductId = p.ProductId,
                    Title = p.Title,
                    Description = p.Description,
                    BasePrice = p.BasePrice,
                    Rating = p.Rating,
                    DiscountedPrice = p.DiscountedPrice,
                    BasePicture = p.BasePicture,
                    IsFavorite = p.ProductFavourites
                        .Any(f => f.CustomerId == customerId && f.IsFavourite==true) // Simplified condition
                })
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<ShopViewByid>> GetShopFavouritesByCustomerIdAsync(int customerId)
        {
            var favouriteShops = await _shopfavouriteRepository.GetTableNoTracking()
    .Where(f => f.CustomerId == customerId && f.IsFavourite == true)
    .Include(f => f.Shop)
        .ThenInclude(s => s.ShopCategory)
    .Include(f => f.Shop)
        .ThenInclude(s => s.Products)
    .ToListAsync();
            var shopMapper = _mapper.Map<IReadOnlyCollection<ShopViewByid>>(favouriteShops);
            return shopMapper;
        
        }

        public async Task<IEnumerable<ShopWithFavoriteDto>> GetShopsWithFavoritesAsync(int customerId)
        {
            return await _shopRepository.GetTableNoTracking()
                .Select(s => new ShopWithFavoriteDto
                {
                    Id = s.Id,
                    PhotoUrl = s.PhotoUrl,
                    BaseShopPhotoUrl = s.BaseShopPhotoUrl,
                    Location = s.Location,
                    BusinessName = s.BusinessName,
                    BusinessType = s.BusinessType,
                    Rating = s.Rating,
                    DeliveryTime = s.DeliveryTime,
                    DeliveryPrice = s.DeliveryPrice,

                    IsFavorite = s.ShopFavourites
                        .Any(f => f.CustomerId == customerId && f.IsFavourite == true),

                    // إضافة فئات المحل
                    ShopCategories = s.ShopCategory
                        .Select(sc => new ShopCategoryDto
                        {
                            Id = sc.CategoryId,
                            Name = sc.Category.Name
                        })
                        .ToList()
                })
                .ToListAsync();
        }

       
    }
}
