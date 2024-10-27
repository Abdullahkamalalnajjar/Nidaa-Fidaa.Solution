using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Respository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.ApplyConfiguration(new ShopCategoryConfig());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


       // public DbSet<Trader> Traders { get; set; }
       // public DbSet<Driver> Drivers { get; set; }  
        public DbSet<Shop> Shops { get; set; }  
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
       // public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductFavourite> ProdcutFavourites { get; set; }
        public DbSet<ShopFavourite> ShopFavourites { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
