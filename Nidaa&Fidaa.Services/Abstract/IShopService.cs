using Nidaa_Fidaa.Core.Dtos;
using Nidaa_Fidaa.Core.Dtos.Shop;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Services.Abstract
{
    public interface IShopService
    {
        public Task<ShopViewByid>GetShopByid(int id);
        public Task<Shop> UpdateShop(UpdateShopDto entity);

        public Task<Shop> AddShop(ShopDto entity);

        public Task<IReadOnlyList<ShopViewByid>> GetShops();
        public Task<List<ShopViewDto>> GetShopsByCustomerId(int id);
        public Task<string> DeleteShop(int id);

        public Task<Category> AddCategoryAsync(CategoryDto categoryDto);
        public Task<IReadOnlyCollection<Category>> GetCategories();
        public Task<List<Product>> GetProductsByCategoryAsync(int categoryId);

        #region DynamicQuery
        public Task<IReadOnlyList<ShopViewDto>> GetShopsWithSepc(ISpecification<Shop> spec);
        public Task<Shop> GetShopbyIdWithSepc(ISpecification<Shop> spec);

        #endregion
    }
}
