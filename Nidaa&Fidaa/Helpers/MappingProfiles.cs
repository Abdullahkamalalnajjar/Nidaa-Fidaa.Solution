using AutoMapper;
using Microsoft.AspNetCore.Http;
using Nidaa_Fidaa.Core.Dtos;
using Nidaa_Fidaa.Core.Dtos.Trader;
using Nidaa_Fidaa.Core.Dtos.Driver;
using Nidaa_Fidaa.Core.Dtos.Shop;
using Nidaa_Fidaa.Core.Dtos.Product;
using Nidaa_Fidaa.Core.Entities;
using System.IO;
using Nidaa_Fidaa.Core.Dtos.Basket;
using Nidaa_Fidaa.Core.Dtos.Favourite;
using Nidaa_Fidaa.Core.Dtos.ProductFavourite;

namespace Nidaa_Fidaa.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Map AddCustomer to Customer
            CreateMap<AddTrader, Trader>()
              .ForMember(dest => dest.Id, opt => opt.Ignore());

            #endregion
            
            #region Map AddDriverDto to Driver 
            CreateMap<AddDriverDto, Driver>()
          .ForMember(dest => dest.IDCardPhotoFront, opt => opt.MapFrom(src => SaveFile(src.IDCardPhotoFront, "IDCardPhotosFront")))
          .ForMember(dest => dest.IDCardPhotoBack, opt => opt.MapFrom(src => SaveFile(src.IDCardPhotoBack, "IDCardPhotoBack")))
          .ForMember(dest => dest.FrontViewPhoto, opt => opt.MapFrom(src => SaveFile(src.FrontViewPhoto, "FrontViewPhotos")))
          .ForMember(dest => dest.RearViewPhoto, opt => opt.MapFrom(src => SaveFile(src.RearViewPhoto, "RearViewPhotos")))
          .ForMember(dest => dest.FullViewWithPlatePhoto, opt => opt.MapFrom(src => SaveFile(src.FullViewWithPlatePhoto, "FullViewWithPlatePhotos")))
          .ForMember(dest => dest.DriverLicensePhoto, opt => opt.MapFrom(src => SaveFile(src.DriverLicensePhoto, "DriverLicensePhotos")));
            #endregion

            #region Map ShopDto to Shop 
            CreateMap<ShopDto, Shop>()
               .ForMember(dest => dest.BaseShopPhotoUrl, opt => opt.MapFrom(src => SaveFile(src.BaseShopPhotoUrl, "BaseShopPhotoUrl")))
               .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => SaveFile(src.PhotoUrl, "PhotoUrl")))
               .ForMember(dest => dest.ShopCategory, opt => opt.MapFrom(src => src.SelectCatergoryIds.Select(id => new ShopCategory { CategoryId = id }).ToList()));
            #endregion

            #region Map Shop to ShopViewDto
            CreateMap<Shop, ShopViewDto>()
                .ForMember(dest => dest.SelectCatergoryIds, opt =>
                    opt.MapFrom(src => src.ShopCategory
                        .Where(mc => mc.Category != null) // تأكد من أن الفئة ليست null
                        .Select(mc => new ShopCategoryDto
                        {
                            Id = mc.CategoryId,
                            Name = mc.Category.Name ?? "Unknown" // تعيين اسم افتراضي إذا كانت الفئة فارغة
                        })));
            #endregion


            #region Map UpdateShopDto to Shop
            CreateMap<UpdateShopDto, Shop>()
                .ForMember(dest => dest.BaseShopPhotoUrl, opt =>
                    opt.Condition(src => src.BaseShopPhotoUrl != null)) // التحقق من أن الصورة الأساسية ليست null
                .ForMember(dest => dest.BaseShopPhotoUrl, opt =>
                    opt.MapFrom(src => SaveFile(src.BaseShopPhotoUrl, "UpdateBaseShopPhotoUrl")))
                .ForMember(dest => dest.ShopCategory, opt =>
                    opt.Condition(src => src.SelectCatergoryIds != null && src.SelectCatergoryIds.Any())) // التحقق من أن القائمة ليست null أو فارغة
                .ForMember(dest => dest.ShopCategory, opt =>
                    opt.MapFrom(src => src.SelectCatergoryIds.Select(id => new ShopCategory { CategoryId = id }).ToList()));
            #endregion

            #region AddMoreDetailsDto to Product
           
            #endregion

            // لا تقم بتحديث ShopCategory باستخدام AutoMapper، لكن يمكنك استخدامه لتحديث الخصائص الأخرى
            CreateMap<UpdateShopDto, Shop>()
                .ForMember(dest => dest.BaseShopPhotoUrl, opt =>
                    opt.Condition(src => src.BaseShopPhotoUrl != null))       
                .ForMember(dest => dest.Rating, opt =>
                    opt.Condition(src => src.Rating != null))  
                .ForMember(dest => dest.DeliveryPrice, opt =>
                    opt.Condition(src => src.DeliveryPrice != null))      
                .ForMember(dest => dest.DeliveryTime, opt =>
                    opt.Condition(src => src.DeliveryTime != null)) 

                .ForMember(dest => dest.BaseShopPhotoUrl, opt =>
                    opt.MapFrom(src => SaveFile(src.BaseShopPhotoUrl, "UpdateBaseShopPhotoUrl")))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.BusinessName))
                .ForMember(dest => dest.BusinessType, opt => opt.MapFrom(src => src.BusinessType));


            #region ;k;k
            // Mapping between ShopFavourite and ShopViewByid
            CreateMap<ShopFavourite, ShopViewByid>()
       .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Shop.Id))
       .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Shop.PhotoUrl))
       .ForMember(dest => dest.BaseShopPhotoUrl, opt => opt.MapFrom(src => src.Shop.BaseShopPhotoUrl))
       .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Shop.Location))
       .ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.Shop.BusinessName))
       .ForMember(dest => dest.BusinessType, opt => opt.MapFrom(src => src.Shop.BusinessType))
       .ForMember(dest => dest.ShopCategories, opt => opt.MapFrom(src => src.Shop.ShopCategory))
       .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Shop.Products));

            CreateMap<ShopCategory, ShopCategoryDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
            #endregion


            #region  Map CategoryDto To Category
            CreateMap<CategoryDto, Category>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.Image, opt=>opt.MapFrom(src => SaveFile(src.Image, "CategoryImage")));
               

            #endregion

            #region Map  AddProductDto to Product
            CreateMap<AddProductDto, Product>()
      .ForMember(dest => dest.BasePicture, opt => opt.MapFrom(src => SaveFile(src.BaseImage, "BaseImages")))
      .ForMember(dest => dest.Images, opt => opt.MapFrom(src => SaveFilesAsImages(src.Images, "ProductImages")));
            #endregion


            #region Map UpdateProductDto to product
            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.BasePicture, opt =>
                    opt.Condition(src => src.BaseImage != null))   
                .ForMember(dest => dest.DeliveryTime, opt =>
                    opt.Condition(src => src.DeliveryTime != null)) 
                .ForMember(dest => dest.DeliveryPrice, opt =>
                    opt.Condition(src => src.DeliveryPrice != null))   
                .ForMember(dest => dest.Rating, opt =>
                    opt.Condition(src => src.Rating != null))  // تحقق من أن الصورة ليست null
                .ForMember(dest => dest.BasePicture, opt =>
                    opt.MapFrom(src => SaveFile(src.BaseImage, "UpdateBaseImages")))
                .ForMember(dest => dest.Images, opt =>
                    opt.Condition(src => src.Images != null && src.Images.Any()))  // تحقق من أن القائمة ليست null أو فارغة
                .ForMember(dest => dest.Images, opt =>
                    opt.MapFrom(src => SaveFilesAsImages(src.Images, "UpdateProductImages")));

            #endregion


            #region Mapp من AddProductAdditionDto إلى ProductAddition
            CreateMap<AddProductAdditionDto, ProductAddition>().ReverseMap();

            #endregion


            #region Map product to  productViewDtoByid
            CreateMap<Product, ProductViewDtoByid>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.ProductSizes, opt => opt.MapFrom(src => src.ProductSizes))
            .ForMember(dest => dest.ProductAdditions, opt => opt.MapFrom(src => src.ProductAdditions))
            .ForMember(dest => dest.Favourites, opt => opt.MapFrom(src => src.ProductFavourites));

            // Map for nested objects
            CreateMap<ProductSize, ProductSizeViewByid>();
            CreateMap<ProductAddition, ProductAdditionViewByid>();
            CreateMap<ProductFavourite, ProductFavouriteDto>();
            #endregion

            #region Map FavouriteDto to Favourite
            CreateMap<ProductFavouriteDto, ProductFavourite>().ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId ,opt => opt.Ignore());
            #endregion


            // Mapping من ProductSizeDto إلى AddProductSize
            CreateMap<ProductSizeDto, ProductSize>().ReverseMap();


            // Mapping for ProductAddition
            CreateMap<UpdateProductAdditionDto, ProductAddition>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id is usually not updated

            // Mapping for ProductSize
            CreateMap<UpdateProductSizeDto, ProductSize>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 
            
            // Id is usually not updated          // Mapping for ProductSize
            CreateMap<BasketItemDto, BasketItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id is usually not updated

            // Mapping for CustomerDto to Customer
            CreateMap<CustomerDto,Customer>().ForMember(dest=>dest.Id, opt => opt.Ignore());

            CreateMap<Basket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();



            // تكوين التعيين بين Shop و ShopViewByid
            CreateMap<Shop, ShopViewByid>()
                .ForMember(dest => dest.ShopCategories, opt => opt.MapFrom(src => src.ShopCategory.Select(sc => new ShopCategoryDto
                {
                    Id = sc.CategoryId,
                    Name = sc.Category.Name,
                    Products = sc.Category.Products.Select(p => new ProductDto
                    {
                        ProductId = p.ProductId,
                        Title = p.Title,
                        Description = p.Description,
                        BasePrice = p.BasePrice,
                        DiscountedPrice = p.DiscountedPrice,
                        BasePicture = p.BasePicture
                        // قم بإضافة المزيد من الخصائص حسب الحاجة
                    }).ToList()
                }).ToList()));

            // تكوين التعيين بين Product و ProductDto
            CreateMap<Product, ProductDto>();



            // تكوين التعيين بين ShopCategory و ShopCategoryDto
            CreateMap<ShopCategory, ShopCategoryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name));







            CreateMap<Shop, ShopViewByid>()
           .ForMember(dest => dest.ShopCategories, opt => opt.MapFrom(src => src.ShopCategory.Select(sc => sc.Category)));

            CreateMap<Category, ShopCategoryDto>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductSizes, opt => opt.MapFrom(src => src.ProductSizes))
                .ForMember(dest => dest.ProductAdditions, opt => opt.MapFrom(src => src.ProductAdditions));


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

    }
}
