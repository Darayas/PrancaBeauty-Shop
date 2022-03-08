using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsMediaAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.ProductReviewsMedia
{
    public class tblProductReviewsMediaConfiguration : IEntityTypeConfiguration<tblProductReviewsMedia>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProductReviewsMedia> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductReviewsId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.FileId).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblProductReviews)
                   .WithMany(a => a.tblProductReviewsMedia)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductReviewsId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblFiles)
                   .WithMany(a => a.tblProductReviewsMedia)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.FileId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
