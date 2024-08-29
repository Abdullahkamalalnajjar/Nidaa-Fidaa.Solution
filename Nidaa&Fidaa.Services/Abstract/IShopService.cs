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
        public Task<Shop> UpdateShop(UpdateShopDto entity);

        public Task<Shop> AddShop(ShopDto entity);

        public Task<IReadOnlyCollection<Shop>> GetShops();
        public Task<List<Shop>> GetShopsByCustomerId(int id);
        public Task<string> DeleteShop(int id);

        public Task<Category> AddCategoryAsync(CategoryDto categoryDto);
        public Task<IReadOnlyCollection<Category>> GetCategories();
    }
}
