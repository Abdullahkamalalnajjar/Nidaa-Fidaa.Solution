using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        public async Task<Product> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            // العثور على المنتج الحالي من قاعدة البيانات
            var existingProduct = await _productRepo.GetTableNoTracking()
                                                    .Where(m => m.ProductId == updateProductDto.Id)
                                                    .Include(m => m.ProductAdditions)
                                                    .Include(m => m.ProductSizes)
                                                    .Include(m => m.Images) // إضافة Include للصور المرتبطة
                                                    .FirstOrDefaultAsync();

            if (existingProduct != null)
            {
                // التحقق من كل خاصية وتحديثها فقط إذا كانت غير null
                if (updateProductDto.Title != null)
                {
                    existingProduct.Title = updateProductDto.Title;
                }

                if (updateProductDto.Description != null)
                {
                    existingProduct.Description = updateProductDto.Description;
                }

                if (updateProductDto.BasePrice.HasValue)
                {
                    existingProduct.BasePrice = updateProductDto.BasePrice.Value;
                }

                if (updateProductDto.DiscountedPrice.HasValue)
                {
                    existingProduct.DiscountedPrice = updateProductDto.DiscountedPrice.Value;
                }

                if (updateProductDto.BaseImage != null)
                {
                    existingProduct.BasePicture = SaveFile(updateProductDto.BaseImage, "UpdateBaseImages");
                }

                if (updateProductDto.CategoryId.HasValue)
                {
                    existingProduct.CategoryId = updateProductDto.CategoryId.Value;
                }

                if (updateProductDto.ShopId.HasValue)
                {
                    existingProduct.ShopId = updateProductDto.ShopId.Value;
                }

                if (updateProductDto.DeliveryPrice.HasValue) {

                    existingProduct.DeliveryPrice = updateProductDto.DeliveryPrice.Value;
                }
                if (updateProductDto.DeliveryTime.HasValue)
                {

                    existingProduct.DeliveryTime = updateProductDto.DeliveryTime.Value;
                }
                if (updateProductDto.Rating.HasValue)
                {

                    existingProduct.Rating = updateProductDto.Rating.Value;
                }

                if (updateProductDto.Images != null && updateProductDto.Images.Any())
                {
                    existingProduct.Images = SaveFilesAsImages(updateProductDto.Images, "UpdateProductImages");
                }

                // تحديث المنتج في قاعدة البيانات
                await _productRepo.UpdateAsync(existingProduct);

                // إرجاع المنتج المحدث
                return existingProduct;
            }

            // إذا لم يتم العثور على المنتج، إرجاع null
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

        public async Task<ProductSize> UpdateProductSizeAsync(UpdateProductSizeDto updateProductSizeDto)
        {
            // تحقق من وجود الكيان الذي تريد تحديثه بناءً على معرف المنتج
            var existingProductSize = await _productSize
                .GetTableNoTracking()
                .Where(ps => ps.Id == updateProductSizeDto.Id) // التحقق باستخدام المعرف الصحيح
                .FirstOrDefaultAsync();

            if (existingProductSize == null)
            {
                return null; // إذا لم يتم العثور على الحجم، أرجع null
            }

            existingProductSize.Size = updateProductSizeDto.Size;
            existingProductSize.Price = updateProductSizeDto.Price;

            await _productSize.UpdateAsync(existingProductSize);
            return existingProductSize;
        }


        public async Task<ProductViewDtoByid> GetProductByIdWithSpec(ISpecification<Product> spec)
        {
            var product = await _productRepo.GetByIdWithSpecAsync(spec);
            if (product == null)
            {
                return null;
            }

            var productMapper = _mapper.Map<ProductViewDtoByid>(product);
            return productMapper;
        }


        private string SaveFile(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                return null;

            // Define the path to save the file
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


        private List<Image> SaveFilesAsImages(List<IFormFile> files, string folderName)
        {
            var images = new List<Image>();

            foreach (var file in files)
            {
                var fileUrl = SaveFile(file, folderName);
                if (fileUrl != null)
                {
                    images.Add(new Image { Path = fileUrl });
                }
            }

            return images;
        }

        public async Task<bool> DeleteProductSizeAsync(int productSizeId)
        {
            var productSize = await _productSize.GetTableNoTracking().Where(ps=>ps.Id.Equals(productSizeId)).FirstOrDefaultAsync();
            if (productSize == null) return false;

            await _productSize.DeleteAsync(productSize);
            return true;
        }

        public async Task<bool> DeleteProductAdditionAsync(int productAdditionId)
        {
            var productAddition = await _productAdditionRepository.GetTableNoTracking().Where(ps => ps.Id.Equals(productAdditionId)).FirstOrDefaultAsync();
            if (productAddition == null) return false;

            await _productAdditionRepository.DeleteAsync(productAddition);
            return true;
        }

        public async Task<Product> SearchProductAsync(ISpecification<Product> spec)
        {

            var product =   await _productRepo.GetSingleWithSpecAsync(spec);

            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsInPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var products = await _productRepo.GetTableNoTracking()
                .Where(p=>p.BasePrice>=minPrice && p.BasePrice<=maxPrice)
                .OrderByDescending(p=>p.BasePrice)
                .ToListAsync();
            if (products==null)
            {
                return null;
            }
            return products;
        }

    }
}
