using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nidaa_Fidaa.Core.Entities.Identity;
using Nidaa_Fidaa.Core.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nidaa_Fidaa.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        public async Task<string> CreateTokenAsync(IdentityUser user, UserManager<IdentityUser> userManager)
        {
            // Private Claims [user-defined]
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            // Retrieve the roles for the current user
            var userRoles = await userManager.GetRolesAsync(user);

            foreach ( var role in userRoles )
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Add specific claims based on user type
            if ( user is CustomerIdentity )
            {
                authClaims.Add(new Claim("UserType", "Customer"));
            }
            else if ( user is DriverIdentity )
            {
                authClaims.Add(new Claim("UserType", "Driver"));
            }
            else if ( user is TraderIdentity )
            {
                authClaims.Add(new Claim("UserType", "Trader"));
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            // Registered Claims
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:AccessTokenLifeTimeInDay"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
