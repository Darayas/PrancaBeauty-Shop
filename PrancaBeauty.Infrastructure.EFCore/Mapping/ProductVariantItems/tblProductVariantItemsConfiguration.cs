using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.ProductVariantItems
{
    public class tblProductVariantItemsConfiguration : IEntityTypeConfiguration<tblProductVariantItems>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProductVariantItems> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductSellerId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductVariantId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.GuaranteeId).IsRequired(false).HasMaxLength(150);
            builder.Property(a => a.DiscountId).IsRequired(false).HasMaxLength(150);
            builder.Property(a => a.ProductCode).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Value).IsRequired().HasMaxLength(500);

            builder.HasOne(a => a.tblProducts)
                   .WithMany(a => a.tblProductVariantItems)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblProductSellers)
                   .WithMany(a => a.tblProductVariantItems)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductSellerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblProductVariants)
                   .WithMany(a => a.tblProductVariantItems)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductVariantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblGuarantee)
                   .WithMany(a => a.tblProductVariantItems)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.GuaranteeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblProductDiscounts)
                   .WithMany(a => a.tblProductVariantItems)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.DiscountId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
