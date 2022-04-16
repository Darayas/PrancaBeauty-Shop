using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Showcases.SectionFreeItemAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.SectionFreeItem
{
    public class tblSectionFreeItemTranslateConfiguration : IEntityTypeConfiguration<tblSectionFreeItemTranslate>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblSectionFreeItemTranslate> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.LangId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.SectionFreeItemId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.FileId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Title).IsRequired(false).HasMaxLength(100);
            builder.Property(a => a.Url).IsRequired(false).HasMaxLength(500);
            builder.Property(a => a.HtmlText).IsRequired(false);

            builder.HasOne(a => a.tblLanguages)
                   .WithMany(a => a.tblSectionFreeItemTranslate)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LangId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblSectionFreeItems)
                   .WithMany(a => a.tblSectionFreeItemTranslate)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.SectionFreeItemId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblFiles)
                   .WithMany(a => a.tblSectionFreeItemTranslate)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.FileId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
