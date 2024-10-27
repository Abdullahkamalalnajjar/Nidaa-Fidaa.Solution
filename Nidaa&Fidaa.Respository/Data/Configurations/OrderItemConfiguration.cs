using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Respository.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.Quantity)
                   .IsRequired();

            builder.Property(oi => oi.UnitPrice)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(oi => oi.TotalPrice)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
            
            builder.HasOne(oi => oi.Product)
                   .WithMany() 
                   .HasForeignKey(oi => oi.ProductId);

           builder.Navigation(p => p.Product).AutoInclude(false);
        }
    
    }
}
