using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Nidaa_Fidaa.Core.Services;
using Nidaa_Fidaa.Repository.Identity;
using Nidaa_Fidaa.Service;
using System.Text;

namespace Nidaa_Fidaa.Api.Extensions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit=false;
                options.Password.RequireLowercase=false;
                options.Password.RequireUppercase=false;
                options.Password.RequireNonAlphanumeric=false;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Events=new JwtBearerEvents
                {
                    OnMessageReceived=context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        if ( !string.IsNullOrEmpty(accessToken) )
                        {
                            context.Token=accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };

                options.TokenValidationParameters=new TokenValidationParameters
                {
                    ValidateIssuer=true,
                    ValidIssuer=configuration["JWT:ValidIssuer"],
                    ValidateAudience=true,
                    ValidAudience=configuration["JWT:ValidAudience"],
                    ValidateLifetime=true,
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });

            // إعدادات الأذونات بناءً على الأدوار
            services.AddAuthorization(options =>
            {
               
            });

            return services;
        }
    }

}
