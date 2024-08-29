using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Dtos;
using Nidaa_Fidaa.Core.Dtos.Shop;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Repository;
using Nidaa_Fidaa.Core.Specification;
using Nidaa_Fidaa.Services.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twilio.TwiML.Voice;

public class ShopService : IShopService
{
    private readonly IGenericRepository<Shop> _merchantRepository;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Category> _categoryRepository;

    public ShopService(IGenericRepository<Shop> merchantRepository, IMapper mapper, IGenericRepository<Category> categRepo)
    {
        _merchantRepository = merchantRepository;
        _mapper = mapper;
        this._categoryRepository = categRepo;
    }

    public async Task<Shop> AddShop(ShopDto merchantDto)
    {
        var check = await _merchantRepository.GetTableNoTracking().Where(d => d.BusinessName == merchantDto.BusinessName).FirstOrDefaultAsync();
        if (check == null)
        {
            var merchant = _mapper.Map<Shop>(merchantDto);
            await _merchantRepository.AddAsync(merchant);
            return merchant;
        }
        return null;

    }

    public async Task<IReadOnlyCollection<Shop>> GetShops()
    {
        var merchants = await _merchantRepository.GetAllAsync();
        return merchants;
    }

    public async Task<List<Shop>> GetShopsByCustomerId(int id)
    {
        var merchant = await _merchantRepository.GetTableNoTracking()
            .Where(i => i.TraderId == id)
            .Include(mc => mc.ShopCategory).ThenInclude(c => c.Category).Include(p => p.Products).ThenInclude(p=>p.ProductAdditions).Include(P=>P.Products).ThenInclude(p => p.ProductSizes).ToListAsync();
        if (merchant == null)
        {
            return null;
        }
        var merchantsMapper = _mapper.Map<List<Shop>>(merchant);
        return merchantsMapper;
    }

    public async Task<string> DeleteShop(int id)
    {
        var merchant = await _merchantRepository.GetByIdAsync(id);
        if (merchant == null)
        {
            throw new KeyNotFoundException($"Merchant with ID {id} not found.");
        }

        await _merchantRepository.DeleteAsync(merchant);
        return $"Merchant with ID {id} has been deleted.";
    }

    public async Task<Category> AddCategoryAsync(CategoryDto categoryDto)
    {
        var check = await _categoryRepository.GetTableNoTracking().Where(m => m.Name.Equals(categoryDto.Name)).FirstOrDefaultAsync();

        if (check != null)
        {
            return null;
        }
        var category = _mapper.Map<Category>(categoryDto); // Mapping from DTO to entity
        await _categoryRepository.AddAsync(category);
        return category;
    }

    public async Task<IReadOnlyCollection<Category>> GetCategories()
    {
        return await _categoryRepository.GetAllAsync();

    }

    public async Task<Shop> UpdateShop(UpdateShopDto entity)
    {
        var existingMerchant = await _merchantRepository.GetTableNoTracking()
                                                     .Where(m => m.Id == entity.Id)
                                                     .Include(m => m.ShopCategory) 
                                                     .FirstOrDefaultAsync();

        if (existingMerchant != null)
        {
            _mapper.Map(entity, existingMerchant);


            if (entity.SelectCatergoryIds != null && entity.SelectCatergoryIds.Any())
            {
                existingMerchant.ShopCategory.Clear();

                foreach (var categoryId in entity.SelectCatergoryIds)
                {
                    existingMerchant.ShopCategory.Add(new ShopCategory
                    {
                        ShopId = existingMerchant.Id,
                        CategoryId = categoryId
                    });
                }
      
            }

            await _merchantRepository.UpdateAsync(existingMerchant);
            return existingMerchant;

        }
        return null;

    }
}


