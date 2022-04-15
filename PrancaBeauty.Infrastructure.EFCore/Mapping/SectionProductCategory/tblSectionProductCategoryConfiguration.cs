using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Domin.Showcases.SectionFreeItemAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrancaBeauty.Domin.Showcases.SectionProductCategoryAgg.Entities;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.SectionProductCategory
{
    public class tblSectionProductCategoryConfiguration : IEntityTypeConfiguration<tblSectionProductCategory>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblSectionProductCategory> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ShowcaseTabSectionId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.CategoryId).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblShowcaseTabSections)
                   .WithMany(a => a.tblSectionProductCategory)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ShowcaseTabSectionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblCategory)
                   .WithMany(a => a.tblSectionProductCategory)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
