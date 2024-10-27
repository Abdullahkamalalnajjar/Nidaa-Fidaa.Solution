using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Entities.Identity;

namespace Nidaa_Fidaa.Repository.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        public DbSet<DriverIdentity> DriverIdentities { get; set; }
        public DbSet<TraderIdentity> TraderIdentities { get; set; }
        public DbSet<CustomerIdentity> CustomerIdentities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

           
        }
    }
}
