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
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            // Configure primary key
            builder.HasKey(bi => bi.Id);

            // Configure properties
            builder.Property(bi => bi.Quantity)
                   .IsRequired();

            builder.Property(bi => bi.UnitPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // Configure foreign key relationship
            builder.HasOne(bi => bi.Basket)
                   .WithMany(b => b.Items)
                   .HasForeignKey(bi => bi.BasketId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
