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
    public class TraderConfiguration : IEntityTypeConfiguration<Trader>
    {
        public void Configure(EntityTypeBuilder<Trader> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
            .ValueGeneratedOnAdd(); // Ensures the ID is auto-generated
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Governorate)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.Municipality)
                .IsRequired()
                .HasMaxLength(100);
        

            builder.Property(c => c.CommercialRegistrationNumber)
                .HasMaxLength(50);
            builder.Property(c => c.TradeActivityName
            ).IsRequired()
            .HasMaxLength(100);


      
        }
    }
}
