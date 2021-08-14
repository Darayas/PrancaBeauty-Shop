using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Product.ProductPropertiesValuesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.ProductPropertiesValues
{
    public class tblProductPropertiesValuesConfiguration : IEntityTypeConfiguration<tblProductPropertiesValues>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProductPropertiesValues> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductPropertiesId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Value).IsRequired().HasMaxLength(3000);

            builder.HasOne(a => a.tblProductPropertis)
                   .WithMany(a => a.tblProductPropertiesValues)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductPropertiesId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblProducts)
                  .WithMany(a => a.tblProductPropertiesValues)
                  .HasPrincipalKey(a => a.Id)
                  .HasForeignKey(a => a.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
