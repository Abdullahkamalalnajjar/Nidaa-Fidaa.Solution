using Nidaa_Fidaa.Core.Dtos.Favourite;
using Nidaa_Fidaa.Core.Dtos.Shop;
using Nidaa_Fidaa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Services.Abstract
{
    public interface IFavouriteService
    {
        public Task<ProductFavourite> AddOrRemoveProductFromFavouriteAsync(int customerId, int productId);
        public Task<ShopFavourite> AddOrRemoveShopFromFavouriteAsync(int customerId, int shopId);

        public Task<IReadOnlyCollection<ProductFavourite>> GetFavouriteAsync ();
        public  Task<IEnumerable<ProductWithFavoriteDto>> GetProductsWithFavoritesAsync(int customerId);
        public  Task<IEnumerable<ShopWithFavoriteDto>> GetShopsWithFavoritesAsync(int customerId);

        public Task<IReadOnlyCollection<Product>> GetProductFavouritesByCustomerIdAsync(int customerId);
        public Task<IReadOnlyCollection<ShopViewByid>> GetShopFavouritesByCustomerIdAsync(int customerId);

    }
}
