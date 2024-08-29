using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nidaa_Fidaa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nidaa_Fidaa.Respository.Data.Configurations
{
    internal class ShopCategoryConfig : IEntityTypeConfiguration<ShopCategory>
    {
        public void Configure(EntityTypeBuilder<ShopCategory> builder)
        {
            builder.HasKey(ts => new
            {
                ts.ShopId,
                ts.CategoryId,
            });

            builder.HasOne(ts => ts.Category).WithMany(t => t.ShopCategory).HasForeignKey(ts => ts.CategoryId);
            builder.HasOne(ts => ts.Shop).WithMany(t => t.ShopCategory).HasForeignKey(ts => ts.ShopId);

        }
    }
}
