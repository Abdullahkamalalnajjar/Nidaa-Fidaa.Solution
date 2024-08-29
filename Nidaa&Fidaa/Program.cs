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
using Nidaa_Fidaa.Core.Twilio;
using Nidaa_Fidaa.Services.Abstract;
using Nidaa_Fidaa.Services.Implmentaion;


namespace Nidaa_Fidaa
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Barear

            #endregion


            #region Auth

            var key = new byte[32]; // 256-bit key
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            }); ;
            #endregion


            #region Cfg Twilio
            builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));
            builder.Services.AddSingleton<WhatsAppService>();
            #endregion


            #region cfg igeneric
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion

            #region ConnectionDatabase
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


            #endregion


            var app = builder.Build();

          
            #region Update-Database
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var ILoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
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

            app.UseAuthorization();

            app.UseStaticFiles();


            app.MapControllers();
      


            app.Run();
        }
    }
}
