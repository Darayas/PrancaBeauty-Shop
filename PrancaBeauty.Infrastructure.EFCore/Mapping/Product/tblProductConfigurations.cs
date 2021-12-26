using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Product
{
    public class tblProductConfigurations : IEntityTypeConfiguration<tblProducts>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProducts> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.TopicId).IsRequired(false).HasMaxLength(150);
            builder.Property(a => a.CategoryId).IsRequired(false).HasMaxLength(150);
            builder.Property(a => a.AuthorUserId).IsRequired().HasMaxLength(450);
            builder.Property(a => a.LangId).IsRequired().HasMaxLength(450);
            builder.Property(a => a.UniqueNumber).IsRequired().HasMaxLength(250);
            builder.Property(a => a.Name).IsRequired(false).HasMaxLength(250);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(250);
            builder.Property(a => a.Description).IsRequired(false);
            builder.Property(a => a.MetaTagDescreption).HasMaxLength(300).IsRequired(false);
            builder.Property(a => a.MetaTagKeyword).HasMaxLength(300).IsRequired(false);
            builder.Property(a => a.MetaTagCanonical).HasMaxLength(150).IsRequired(false);
            builder.Property(a => a.IncompleteReason).HasMaxLength(500).IsRequired(false);

            builder.HasOne(a => a.tblAuthorUser)
                   .WithMany(a => a.tblProducts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.AuthorUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblCategory)
                   .WithMany(a => a.tblProducts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblLanguage)
                   .WithMany(a => a.tblProducts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LangId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblProductTopic)
                  .WithMany(a => a.tblProducts)
                  .HasPrincipalKey(a => a.Id)
                  .HasForeignKey(a => a.TopicId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
