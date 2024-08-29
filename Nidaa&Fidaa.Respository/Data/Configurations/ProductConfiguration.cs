using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nidaa_Fidaa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Respository.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Configure the primary key
            builder.HasKey(p => p.Id);



            // Configure properties
            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100);
            // Configure Indexes if needed
            builder.HasIndex(p => p.Title)
                .IsUnique();

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.BasePrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.DiscountedPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.BasePicture);

          




            #region Relationship With Shop
            builder.HasOne(p => p.Shop)
.WithMany(m => m.Products)
.HasForeignKey(p => p.ShopId)
.OnDelete(DeleteBehavior.Cascade); 
            #endregion
            #region Relationship with Image
            builder.HasMany(p => p.Images)
.WithOne(pi => pi.Product)
.HasForeignKey(pi => pi.ProductId);
            #endregion

            #region Relationship with ProductSize

            builder.HasMany(p => p.ProductSizes)
         .WithOne(ps => ps.Product)
          .HasForeignKey(ps => ps.ProductId)
           .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region  Relationship with ProductAddition
            builder.HasMany(p => p.ProductAdditions)
     .WithOne(ps => ps.Product)
     .HasForeignKey(ps => ps.ProductId)
     .OnDelete(DeleteBehavior.Cascade);
            #endregion

           



        }
    }
}
