using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace Nidaa_Fidaa.Respository.Data.Configurations
{
    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
           

           
            builder.HasKey(m => m.Id);

            builder.Property(m => m.PhotoUrl)
                .HasMaxLength(255) 
                .IsRequired();


            builder.Property(m => m.BaseShopPhotoUrl)
              .HasMaxLength(255)
              .IsRequired();

            builder.Property(m => m.Location)
                .IsRequired(); 


            builder.Property(m => m.BusinessName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(m => m.BusinessType)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.DeliveryPrice)
           .HasColumnType("decimal(18,2)");

            builder.Property(p => p.DeliveryTime)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Rating)
                .HasColumnType("decimal(18,2)");

        }
    }

}



