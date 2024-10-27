using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Entities;

public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        // Configure primary key
        builder.HasKey(bi => bi.Id);

        // تأكيد عدم وجود قيد فريد على ProductSizeId بمفرده
        builder.HasIndex(bi => new { bi.BasketId, bi.ProductSizeId })
               .IsUnique(false);

        // Configure properties
        builder.Property(bi => bi.Quantity)
               .IsRequired();

        //builder.Property(bi => bi.UnitPrice)
        //       .IsRequired()
        //       .HasColumnType("decimal(18,2)");
        builder.HasMany(bi => bi.Additions)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);

        builder.Property(bi => bi.TotalPrice)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        // Configure foreign key relationship with Basket
        builder.HasOne(bi => bi.Basket)
               .WithMany(b => b.Items)
               .HasForeignKey(bi => bi.BasketId)
               .OnDelete(DeleteBehavior.Cascade);

        // Configure foreign key relationship with Product
        builder.HasOne(bi => bi.Product)
               .WithMany()
               .HasForeignKey(bi => bi.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

        // Configure foreign key relationship with ProductSize
        builder.HasOne(bi => bi.ProductSize)
               .WithMany()
               .HasForeignKey(bi => bi.ProductSizeId)
               .OnDelete(DeleteBehavior.Restrict);

         builder.Ignore(bi => bi.TotalPrice);
    }
}
