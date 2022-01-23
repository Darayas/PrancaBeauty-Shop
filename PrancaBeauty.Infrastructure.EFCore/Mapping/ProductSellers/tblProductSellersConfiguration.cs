using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Product.ProductSellerAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.ProductSellers
{
    public class tblProductSellersConfiguration : IEntityTypeConfiguration<tblProductSellers>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProductSellers> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.SellerId).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblProducts)
                   .WithMany(a => a.tblProductSellers)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblSellers)
                   .WithMany(a => a.tblProductSellers)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.SellerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
