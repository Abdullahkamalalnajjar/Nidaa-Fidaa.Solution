using Microsoft.AspNetCore.Identity;
using Nidaa_Fidaa.Core.Entities.Identity;

namespace Nidaa_Fidaa.Core.Services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(IdentityUser user, UserManager<IdentityUser> userManager);
    }
}
