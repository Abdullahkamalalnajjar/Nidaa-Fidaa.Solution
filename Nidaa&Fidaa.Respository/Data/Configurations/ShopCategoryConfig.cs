//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Nidaa_Fidaa.Core.Entities;

//namespace Nidaa_Fidaa.Repository.Data.Configurations
//{
//    public class ShopCategoryConfig : IEntityTypeConfiguration<ShopCategory>
//    {
//        public void Configure(EntityTypeBuilder<ShopCategory> builder)
//        {
//            // Define composite key
//            builder.HasKey(sc => new
//            {
//                sc.ShopId,
//                sc.CategoryId,
//            });

//            // Configure relationships
//            builder.HasOne(sc => sc.Shop)
//                   .WithMany() // Ensure navigation property matches the Shop entity
//                   .HasForeignKey(sc => sc.ShopId);

//            builder.HasOne(sc => sc.Category)
//                   .WithMany() // Ensure navigation property matches the Category entity
//                   .HasForeignKey(sc => sc.CategoryId);
//        }
//    }
//}
