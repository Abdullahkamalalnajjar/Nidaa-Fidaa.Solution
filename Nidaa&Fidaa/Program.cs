using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Repository;
using Nidaa_Fidaa.Helpers;
using Nidaa_Fidaa.Respository;
using Nidaa_Fidaa.Respository.Data;
using System.Globalization;

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
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
