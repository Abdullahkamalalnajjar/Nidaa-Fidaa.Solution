using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Dtos;
using Nidaa_Fidaa.Core.Dtos.Product;
using Nidaa_Fidaa.Core.Dtos.Shop;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Repository;
using Nidaa_Fidaa.Core.Specification;
using Nidaa_Fidaa.Respository.Data;
using Nidaa_Fidaa.Services.Abstract;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Twilio.TwiML.Voice;

public class ShopService : IShopService
{
    private readonly IGenericRepository<Shop> _shopRepository;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly ApplicationDbContext _context;

    public ShopService(IGenericRepository<Shop> shopRepository, IMapper mapper, IGenericRepository<Category> categRepo , ApplicationDbContext context)
    {
        _shopRepository = shopRepository;
        _mapper = mapper;
        this._categoryRepository = categRepo;
       _context = context;
    }

    public async Task<Shop> AddShop(ShopDto merchantDto)
    {
        var check = await _shopRepository.GetTableNoTracking().Where(d => d.BusinessName == merchantDto.BusinessName).FirstOrDefaultAsync();
        if (check == null)
        {
            var merchant = _mapper.Map<Shop>(merchantDto);
            await _shopRepository.AddAsync(merchant);
            return merchant;
        }
        return null;

    }

    public async Task<IReadOnlyList<ShopViewByid>> GetShops()
    {
        var merchants = await _shopRepository.GetAllAsync();
        var shopMapper = _mapper.Map<IReadOnlyList<ShopViewByid>>(merchants);

        return shopMapper;
    }

    public async Task<List<ShopViewDto>> GetShopsByCustomerId(int id)
    {
        var merchant = await _shopRepository.GetTableNoTracking()
            .Where(i => i.TraderId == id)
            .Include(mc => mc.ShopCategory).ThenInclude(c => c.Category).Include(p => p.Products).ThenInclude(p=>p.ProductAdditions).Include(P=>P.Products).ThenInclude(p => p.ProductSizes).ToListAsync();
        if (merchant == null)
        {
            return null;
        }
        var merchantsMapper = _mapper.Map<List<ShopViewDto>>(merchant);
        return merchantsMapper;
    }

    public async Task<string> DeleteShop(int id)
    {
        var merchant = await _shopRepository.GetByIdAsync(id);
        if (merchant == null)
        {
            throw new KeyNotFoundException($"Merchant with ID {id} not found.");
        }

        await _shopRepository.DeleteAsync(merchant);
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







    public async Task<Shop> UpdateShop(UpdateShopDto dto)
    {
        var shop = await _context.Shops
                                 .Include(s => s.ShopCategory)
                                 .ThenInclude(sc => sc.Category)
                                 .FirstOrDefaultAsync(s => s.Id == dto.Id);

        if (shop == null)
            throw new Exception("Shop not found");

        if (!string.IsNullOrEmpty(dto.BusinessName))
        {
            shop.BusinessName = dto.BusinessName;
        }

        if (!string.IsNullOrEmpty(dto.Location))
        {
            shop.Location = dto.Location;
        }

        if (!string.IsNullOrEmpty(dto.BusinessType))
        {
            shop.BusinessType = dto.BusinessType;
        }


        
        if (dto.PhotoUrl != null)
        {
           
            shop.PhotoUrl =  SaveFile(dto.PhotoUrl,"UpdateShopUrl");
        }

        if (dto.BaseShopPhotoUrl != null)
        {
            
            shop.BaseShopPhotoUrl =  SaveFile(dto.BaseShopPhotoUrl, "UpdateBasePhotoUrl");
        }



        if (dto.DeliveryPrice.HasValue)
        {

            dto.DeliveryPrice = dto.DeliveryPrice.Value;
        }
        if (dto.DeliveryTime.HasValue)
        {

            shop.DeliveryTime = dto.DeliveryTime.Value;
        }
        if (dto.Rating.HasValue)
        {

            shop.Rating = dto.Rating.Value;
        }



        if (dto.SelectCatergoryIds != null && dto.SelectCatergoryIds.Any())
        {
            var existingCategoryIds = shop.ShopCategory.Select(sc => sc.CategoryId).ToList();

        
            var categoriesToAdd = dto.SelectCatergoryIds.Except(existingCategoryIds);

            foreach (var categoryId in categoriesToAdd)
            {
                shop.ShopCategory.Add(new ShopCategory
                {
                    ShopId = shop.Id,
                    CategoryId = categoryId
                });
            }
        }

        await _context.SaveChangesAsync();
        return shop;
    }





    public async Task<ShopViewByid> GetShopByid(int id)
    {
        var existItem = await _shopRepository.GetByIdAsync(id);
        var shopMapper = _mapper.Map<ShopViewByid>(existItem);
        if (existItem != null)
        {
            return shopMapper;
        }
        return null;
    }



    private string SaveFile(IFormFile file, string folderName)
    {
        if (file == null || file.Length == 0)
            return null;

        string uploadsFolder = Path.Combine("wwwroot", folderName);
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        // Ensure the directory exists
        Directory.CreateDirectory(uploadsFolder);

        // Save the file
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(fileStream);
        }

        // Get the server name and base URL
        string serverBaseUrl = "http://nidaafidaa.runasp.net/"; // Replace with your actual server name
        string relativePath = Path.Combine(folderName, uniqueFileName);

        // Return the full URL to the file
        return $"{serverBaseUrl}/{relativePath.Replace("\\", "/")}";
    }



    public async Task<IReadOnlyList<ShopViewDto>> GetShopsWithSepc(ISpecification<Shop> spec)
    {
       var products = await _shopRepository.GetAllWithSpecAsync(spec);   
       var productsMapping = _mapper.Map<IReadOnlyList<ShopViewDto>>(products);
        return productsMapping;
    }

    public async Task<Shop> GetShopbyIdWithSepc(ISpecification<Shop> spec)
    {
        var product= await _shopRepository.GetByIdWithSpecAsync(spec);
        return product;
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await _categoryRepository.GetTableNoTracking()
            .Where(c => c.Id == categoryId)
            .SelectMany(c => c.Products)
            .Include(p=>p.Images)
            .Include(p => p.ProductSizes) 
            .Include(p => p.ProductAdditions) 
            .ToListAsync();

        return products;
    }

}


