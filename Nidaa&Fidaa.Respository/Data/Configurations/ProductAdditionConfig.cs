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
    public class ProductAdditionConfig : IEntityTypeConfiguration<ProductAddition>
    {
        public void Configure(EntityTypeBuilder<ProductAddition> builder)
        {
            builder.Property(pa => pa.Price).IsRequired().HasColumnType("decimal(18,2)");

           
        }
    }
}
