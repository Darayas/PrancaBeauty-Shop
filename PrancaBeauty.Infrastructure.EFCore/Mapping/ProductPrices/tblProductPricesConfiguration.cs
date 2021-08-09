using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Product.ProductPricesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.ProductPrices
{
    public class tblProductPricesConfiguration : IEntityTypeConfiguration<tblProductPrices>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProductPrices> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.UserId).IsRequired().HasMaxLength(450);

            builder.HasOne(a => a.tblProducts)
                   .WithMany(a => a.tblProductPrices)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblUsers)
                   .WithMany(a => a.tblProductPrices)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
