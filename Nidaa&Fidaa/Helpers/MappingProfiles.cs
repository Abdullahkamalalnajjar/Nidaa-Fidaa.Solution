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

namespace Nidaa_Fidaa.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Map QueryOfAddCustomer to Customer
            CreateMap<AddTrader, Trader>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
             

            // Map AddDriverDto to Driver 
            CreateMap<AddDriverDto, Driver>()
                .ForMember(dest => dest.IDCardPhotoFront, opt => opt.MapFrom(src => SaveFile(src.IDCardPhotoFront, "IDCardPhotosFront")))
                .ForMember(dest => dest.IDCardPhotoBack, opt => opt.MapFrom(src => SaveFile(src.IDCardPhotoBack, "IDCardPhotoBack")))
                .ForMember(dest => dest.FrontViewPhoto, opt => opt.MapFrom(src => SaveFile(src.FrontViewPhoto, "FrontViewPhotos")))
                .ForMember(dest => dest.RearViewPhoto, opt => opt.MapFrom(src => SaveFile(src.RearViewPhoto, "RearViewPhotos")))
                .ForMember(dest => dest.FullViewWithPlatePhoto, opt => opt.MapFrom(src => SaveFile(src.FullViewWithPlatePhoto, "FullViewWithPlatePhotos")))
                .ForMember(dest => dest.DriverLicensePhoto, opt => opt.MapFrom(src => SaveFile(src.DriverLicensePhoto, "DriverLicensePhotos")));
            //Map MerchantDto to Merchant 
            CreateMap<ShopDto, Shop>()
            //    .ForMember(dest => dest.MerchantPhotoUrl, opt => opt.MapFrom(src => SaveFile(src.MerchantPhotoUrl, "MerchantPhotoUrl")))
                .ForMember(dest => dest.ShopPhotoUrl, opt => opt.MapFrom(src => SaveFile(src.ShopPhotoUrl, "ShopPhotoUrl")))
                .ForMember(dest => dest.ShopCategory, opt => opt.MapFrom(src => src.SelectCatergoryIds.Select(id => new ShopCategory { CategoryId = id }).ToList()));
            //Map Marchant To MerchantDto
            // Mapping for Merchant to MerchantViewDto
            CreateMap<Shop, ShopViewDto>()
                .ForMember(dest => dest.SelectCatergoryIds, opt =>
                    opt.MapFrom(src => src.ShopCategory.Select(mc => new ShopCategoryDto
                    {
                        Id = mc.CategoryId,
                        Name = mc.Category.Name // Assuming Category has a Name property
                    })));

            // Map UpdateMerchantDto to Merchant
            CreateMap<UpdateShopDto, Shop>()
          //     .ForMember(dest => dest.MerchantPhotoUrl, opt => opt.MapFrom(src => SaveFile(src.MerchantPhotoUrl, "UpdateMerchantPhotoUrl")))
               .ForMember(dest => dest.ShopPhotoUrl, opt => opt.MapFrom(src => SaveFile(src.ShopPhotoUrl, "UpdateShopPhotoUrl")))
               .ForMember(dest => dest.ShopCategory, opt => opt.MapFrom(src => src.SelectCatergoryIds.Select(id => new ShopCategory { CategoryId = id }).ToList()));

            // Map CategoryDto To Category
            CreateMap<CategoryDto, Category>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Map from AddProductDto to Product
            CreateMap<AddProductDto, Product>()
                .ForMember(dest => dest.BasePicture, opt => opt.MapFrom(src => SaveFile(src.BaseImage, "BaseImages")))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => SaveFilesAsImages(src.Images, "ProductImages")));




            //map from UpdateProductDto to product
            CreateMap<UpdateProductDto, Product>()
            .ForMember(dest => dest.BasePicture, opt => opt.MapFrom(src => SaveFile(src.BaseImage, "UpdateBaseImages")))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => SaveFilesAsImages(src.Images, "UpdateProductImages")));
 
            // Mapping من AddProductAdditionDto إلى ProductAddition
            CreateMap<AddProductAdditionDto, ProductAddition>().ReverseMap();



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
            string serverBaseUrl = "http://infocustomer.runasp.net/"; // Replace with your actual server name
            string relativePath = Path.Combine(folderName, uniqueFileName);

            // Return the full URL to the file
            return $"{serverBaseUrl}/{relativePath.Replace("\\", "/")}";
        }

    }
}
