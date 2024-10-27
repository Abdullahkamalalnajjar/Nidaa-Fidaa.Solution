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
    public class ProductFavouriteConfiguration : IEntityTypeConfiguration<ProductFavourite>
    {
        public void Configure(EntityTypeBuilder<ProductFavourite> builder)
        {
            // تعيين المفتاح الأساسي (Primary Key) للجدول
            builder.HasKey(f => f.Id);

            // تعيين العلاقة بين Favourite و Customer
            builder.HasOne(f => f.Customer)
                .WithMany(c => c.ProductFavourites)
                .HasForeignKey(f => f.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); // حذف تلقائي للسجلات المرتبطة

            // تعيين العلاقة بين Favourite و Product
            builder.HasOne(f => f.Product)
                .WithMany(p => p.ProductFavourites)
                .HasForeignKey(f => f.ProductId)
                .OnDelete(DeleteBehavior.Cascade); // حذف تلقائي للسجلات المرتبطة

      
           
        }
    }
}
