using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Domin.Product.ProductVariantAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrancaBeauty.Domin.Showcases.SectionCategories.Entities;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.SectionCategories
{
    public class tblSectionCategoryConfiguration : IEntityTypeConfiguration<tblSectionCategories>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblSectionCategories> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ShowcaseTabSectionId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.CategoryId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);

            builder.HasOne(a => a.tblShowcaseTabSections)
                   .WithMany(a => a.tblSectionCategories)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ShowcaseTabSectionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblCategoris)
                   .WithMany(a => a.tblSectionCategories)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
