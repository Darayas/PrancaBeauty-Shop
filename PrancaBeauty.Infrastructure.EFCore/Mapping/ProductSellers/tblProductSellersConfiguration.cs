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
            builder.Property(a => a.SellerUserId).IsRequired().HasMaxLength(450);
            builder.Property(a => a.Guarantee).IsRequired(false).HasMaxLength(200);
            builder.Property(a => a.SendFrom).IsRequired().HasMaxLength(200);

            builder.HasOne(a => a.tblProducts)
                   .WithMany(a => a.tblProductSellers)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblUsers)
                   .WithMany(a => a.tblProductSellers)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.SellerUserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
