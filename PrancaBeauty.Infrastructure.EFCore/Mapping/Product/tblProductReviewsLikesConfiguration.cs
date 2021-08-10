using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Product
{
    public class tblProductReviewsLikesConfiguration : IEntityTypeConfiguration<tblProductReviewsLikes>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProductReviewsLikes> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductReviewId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.UserId).IsRequired().HasMaxLength(450);

            builder.HasOne(a => a.tblProductReviews)
                   .WithMany(a => a.tblProductReviewsLikes)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductReviewId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblUsers)
                   .WithMany(a => a.tblProductReviewsLikes)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
