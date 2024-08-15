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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

         
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.ProfilePictureUrl)
                .IsRequired()
                .HasMaxLength(100);


            builder.Property(c => c.Governorate)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Zone)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(c => c.TradeActivity)
                 .WithMany()
                 .HasForeignKey(c => c.TradeActivityId);
        }
    }
}
