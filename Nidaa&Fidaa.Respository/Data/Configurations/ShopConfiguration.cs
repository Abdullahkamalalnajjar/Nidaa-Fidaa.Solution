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

            builder.Property(m => m.ShopPhotoUrl)
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


        }
    }

}



