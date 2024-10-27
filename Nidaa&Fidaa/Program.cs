using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nidaa_Fidaa.Core.Repository;
using Nidaa_Fidaa.Helpers;
using Nidaa_Fidaa.Respository;
using Nidaa_Fidaa.Respository.Data;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;
using Nidaa_Fidaa.Services.Implmentaion;
using Nidaa_Fidaa.Services;
using Nidaa_Fidaa.Services.Abstract;
using System.Text.Json.Serialization;
using System.Text.Json;
using Nidaa_Fidaa.Api.Extensions;
using Nidaa_Fidaa.Repository.Identity;
using Microsoft.AspNetCore.Identity;


namespace Nidaa_Fidaa
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

         

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers(); 
            builder.Services.AddHttpClient();
            builder.Services.AddIdentityServices(builder.Configuration);


            #region cfg igeneric
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion

            #region ConnectionDatabase

            builder.Services.AddDbContext<AppIdentityDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });


            builder.Services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));
            });
            #endregion

            #region Config AutoMapper 
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile(typeof(MappingProfiles)));
            #endregion

            #region addDepenances_Service
            builder.Services.AddScoped(typeof(IDriverService), typeof(DriverService));
            builder.Services.AddScoped(typeof(ITraderService), typeof(TraderService));
            builder.Services.AddScoped(typeof(IShopService), typeof(ShopService));
            builder.Services.AddScoped(typeof(IProductService), typeof(ProductService));
            builder.Services.AddScoped(typeof(IBasketService), typeof(BasketService));
            builder.Services.AddScoped(typeof(ICustomerService), typeof(CustomerService));
            builder.Services.AddScoped(typeof(IFavouriteService), typeof(FavouriteService));
            builder.Services.AddScoped(typeof(IFilterService), typeof(FilterService));
            builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));
 

            #endregion


            var app = builder.Build();

          
            #region Update-Database
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var ILoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {

                var identityDbContext = services.GetRequiredService<AppIdentityDbContext>();
                await identityDbContext.Database.MigrateAsync();

                var dbContext = services.GetRequiredService<ApplicationDbContext>();
                await dbContext.Database.MigrateAsync();
                //await DataSeedDbContext.SeedAsync(dbContext);
            }
            catch (Exception ex)
            {

                var logger = ILoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "ِan error Occured during apply the Migration");
            }

            #endregion
            // Configure the HTTP request pipeline.
           // if (app.Environment.IsDevelopment())
         //  {
                app.UseSwagger();
                app.UseSwaggerUI();
         //  }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles();


            app.MapControllers();
      


            app.Run();
        }
    }
}
