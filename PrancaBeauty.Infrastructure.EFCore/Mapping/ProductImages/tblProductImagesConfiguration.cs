using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.Product.ProductImagesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.ProductImages
{
    public class tblProductImagesConfiguration : IEntityTypeConfiguration<tblProductImages>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProductImages> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.FileId).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblProducts)
                   .WithMany(a => a.tblProductImages)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblFiles)
                   .WithOne(a => a.tblProductImages)
                   .HasPrincipalKey<tblFiles>(a => a.Id)
                   .HasForeignKey<tblProductImages>(a => a.FileId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
