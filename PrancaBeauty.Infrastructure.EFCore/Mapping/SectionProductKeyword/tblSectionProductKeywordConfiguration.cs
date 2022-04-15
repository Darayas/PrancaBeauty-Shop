using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Domin.Showcases.SectionProductCategoryAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrancaBeauty.Domin.Showcases.SectionProductKeywordAgg.Entities;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.SectionProductKeyword
{
    public class tblSectionProductKeywordConfiguration : IEntityTypeConfiguration<tblSectionProductKeyword>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblSectionProductKeyword> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ShowcaseTabSectionId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.KeywordId).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblShowcaseTabSections)
                   .WithMany(a => a.tblSectionProductKeyword)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.KeywordId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblKeywords)
                   .WithMany(a => a.tblSectionProductKeyword)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.KeywordId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}