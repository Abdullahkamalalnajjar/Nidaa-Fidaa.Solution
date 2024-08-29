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
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
         

            // Primary key
            builder.HasKey(d => d.Id);

            // Name
            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            // ID Number
            builder.Property(d => d.IDNumber)
                .IsRequired()
                .HasMaxLength(14);

            // Governorate
            builder.Property(d => d.Governorate)
                .IsRequired()
                .HasMaxLength(50);

            // Zone
            builder.Property(d => d.Municipality)
                .IsRequired()
                .HasMaxLength(50);

            // Transportation Type
            builder.Property(d => d.TransportationType)
                .IsRequired()
                .HasMaxLength(20);

        

            // License Plate Number
            builder.Property(d => d.LicensePlateNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(d => d.IDCardPhotoFront)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(d => d.IDCardPhotoBack)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(d => d.FrontViewPhoto)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(d => d.RearViewPhoto)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(d => d.FullViewWithPlatePhoto)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(d => d.DriverLicensePhoto)
                   .HasMaxLength(255)
                   .IsRequired();

        }
    }
}
