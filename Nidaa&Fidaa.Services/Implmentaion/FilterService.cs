using AutoMapper;
using Nidaa_Fidaa.Core.Dtos.Product;
using Nidaa_Fidaa.Core.Dtos.Search;
using Nidaa_Fidaa.Core.Dtos.Shop;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Repository;
using Nidaa_Fidaa.Core.Specification.Handller;
using Nidaa_Fidaa.Services.Abstract;

public class FilterService : IFilterService
{
    private readonly IGenericRepository<Shop> _shopRepository;
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public FilterService(IGenericRepository<Shop> shopRepository, IGenericRepository<Product> productRepository, IMapper mapper)
    {
        _shopRepository = shopRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }


    public async Task<FilterResultDto> FilterAsync(string? searchTerm, decimal? minPrice, decimal? maxPrice)
    {
        var result = new FilterResultDto();

        // Step 1: Fetch and filter shops based on searchTerm if provided
        var shopsQuery = await _shopRepository.GetAllAsync();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            shopsQuery = shopsQuery
                .Where(shop => shop.BusinessName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                            || shop.Location.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Map filtered shops to DTO
        result.Shops = _mapper.Map<List<ShopViewByid>>(shopsQuery);

        // Step 2: Fetch and filter products based on searchTerm and price range
        var productsQuery = await _productRepository.GetAllAsync();

        // Filter by searchTerm if provided
        if (!string.IsNullOrEmpty(searchTerm))
        {
            productsQuery = productsQuery
                .Where(product => product.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                               || product.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Further filter by minPrice and maxPrice if provided
        if (minPrice.HasValue || maxPrice.HasValue)
        {
            productsQuery = productsQuery
                .Where(product => (!minPrice.HasValue || product.DiscountedPrice >= minPrice.Value) &&
                                  (!maxPrice.HasValue || product.DiscountedPrice <= maxPrice.Value))
                .OrderByDescending(product => product.DiscountedPrice)
                .ToList();
        }

        // Map filtered products to DTO
        result.Products = _mapper.Map<List<ProductViewDtoByid>>(productsQuery);

        return result;
    }



}
