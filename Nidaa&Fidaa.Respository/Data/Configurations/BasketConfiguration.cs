using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nidaa_Fidaa.Core;
using Nidaa_Fidaa.Core.Entities;
namespace Nidaa_Fidaa.Respository.Data.Configurations
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            // Configure primary key
            builder.HasKey(b => b.Id);

         

            builder.HasMany(b => b.Items)
                   .WithOne(bi => bi.Basket)
                   .HasForeignKey(bi => bi.BasketId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
