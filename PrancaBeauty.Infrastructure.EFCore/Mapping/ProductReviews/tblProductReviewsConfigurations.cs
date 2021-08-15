using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.ProductReviews
{
    public class tblProductReviewsConfigurations : IEntityTypeConfiguration<tblProductReviews>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProductReviews> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductSellerId).IsRequired(false).HasMaxLength(150);
            builder.Property(a => a.AuthorUserId).IsRequired().HasMaxLength(450);
            builder.Property(a => a.IpAddress).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Text).IsRequired();

            builder.HasOne(a => a.tblUsers)
                   .WithMany(a => a.tblProductReviews)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.AuthorUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblProducts)
                   .WithMany(a => a.tblProductReviews)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblProductSellers)
                   .WithMany(a => a.tblProductReviews)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductSellerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
