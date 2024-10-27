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
    public class ShopFavouriteConfiguration : IEntityTypeConfiguration<ShopFavourite>
    {
        public void Configure(EntityTypeBuilder<ShopFavourite> builder)
        {
            // تعيين المفتاح الأساسي (Primary Key) للجدول
            builder.HasKey(f => f.Id);

            // تعيين العلاقة بين Favourite و Customer
            builder.HasOne(f => f.Customer)
                .WithMany(c => c.ShopFavourites)
                .HasForeignKey(f => f.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); // حذف تلقائي للسجلات المرتبطة

            // تعيين العلاقة بين Favourite و Product
            builder.HasOne(f => f.Shop)
                    .WithMany(p => p.ShopFavourites)
                    .HasForeignKey(f => f.ShopId)
                    .OnDelete(DeleteBehavior.Cascade); // حذف تلقائي للسجلات المرتبطة
        }
    }
}
