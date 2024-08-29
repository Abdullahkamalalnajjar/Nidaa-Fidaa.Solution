using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nidaa_Fidaa.Core.Entities;

public class ProductSizeConfig : IEntityTypeConfiguration<ProductSize>
{
    public void Configure(EntityTypeBuilder<ProductSize> builder)
    {
        // Ensure Price is required and has the correct decimal type
        builder.Property(ps => ps.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        // Ensure Size is required and has a max length
        builder.Property(ps => ps.Size)
            .IsRequired()
            .HasMaxLength(10);
    }
}
