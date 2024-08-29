using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Dtos.Product;
using Nidaa_Fidaa.Core.Entities;
using Nidaa_Fidaa.Core.Repository;
using Nidaa_Fidaa.Core.Specification;
using Nidaa_Fidaa.Respository.Data;
using Nidaa_Fidaa.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Voice;

namespace Nidaa_Fidaa.Services.Implmentaion
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductSize> _productSize;
        private readonly IGenericRepository<ProductAddition> _productAdditionRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public ProductService(IGenericRepository<Product> productRepo, IGenericRepository<ProductSize> productSize,IGenericRepository<ProductAddition> productAddRepo,IMapper mapper, ApplicationDbContext context)
        {
            _productRepo = productRepo;
            _productSize = productSize;
            _productAdditionRepository = productAddRepo;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProductAddition> AddProductAdditionAsync(AddProductAdditionDto addProductAdditionDto)
        {
        
            // إنشاء كائن ProductAddition من DTO
            var productAddition = _mapper.Map<ProductAddition>(addProductAdditionDto);

            // إضافة المنتج المضاف
            await _productAdditionRepository.AddAsync(productAddition);
            return productAddition;
        }


        public async Task<Product> AddProductAsync(AddProductDto productDto)
        {
            var check = await _productRepo.GetTableNoTracking().Where(p => p.Title == productDto.Title).FirstOrDefaultAsync();
            if (check != null)
            {
                return null;
            }

            var product = _mapper.Map<Product>(productDto);

           await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;

        }

        public async Task<ProductSize> AddProductSizeAsync(ProductSizeDto productSizeDto)
        {
         
            // إنشاء كائن ProductAddition من DTO
            var productSize = _mapper.Map<ProductSize>(productSizeDto);

            // إضافة المنتج المضاف
            await _productSize.AddAsync(productSize);
            return productSize;
        }

        public Task<AddProductDto> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Product>> GetProductsWithSepc(ISpecification<Product> spec)
        {
            return await _productRepo.GetAllWithSpecAsync(spec);
        }

        public async Task<Product> UpdateProductAsync(UpdateProductDto updateproductDto)
        {
            var existingProduct = await _productRepo.GetTableNoTracking()
                                                    .Where(m => m.Id == updateproductDto.Id)
                                                    .Include(m => m.ProductAdditions)
                                                    .Include(m => m.ProductSizes)
                                                    .FirstOrDefaultAsync();

            if (existingProduct != null)
            {
                _mapper.Map(updateproductDto, existingProduct);

                await _productRepo.UpdateAsync(existingProduct);
                return existingProduct;
            }

            return null;
        }

        public async Task<ProductAddition> UpdateProductAdditionAsync(UpdateProductAdditionDto updateProductAdditionDto)
        {
            var existingProductAddition = await _productAdditionRepository
                .GetTableNoTracking()
                .Where(pa => pa.Id == updateProductAdditionDto.Id)
                .FirstOrDefaultAsync();

            if (existingProductAddition == null)
            {
                return null; // Product addition not found
            }

            // Update the existing product addition with new values
            _mapper.Map(updateProductAdditionDto, existingProductAddition);

            await _productAdditionRepository.UpdateAsync(existingProductAddition);
            return existingProductAddition;
        }

        public async Task<ProductSize> UpdateProductSizeAsync(ProductSizeDto updateProductSizeDto)
        {
            var existingProductSize = await _productSize
                .GetTableNoTracking()
                .Where(ps => ps.Id == updateProductSizeDto.ProductId)
                .FirstOrDefaultAsync();

            if (existingProductSize == null)
            {
                return null; // Product size not found
            }

            // Update the existing product size with new values
            _mapper.Map(updateProductSizeDto, existingProductSize);

            await _productSize.UpdateAsync(existingProductSize);
            return existingProductSize;
        }
    }
}
