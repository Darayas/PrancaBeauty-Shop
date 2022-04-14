using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Showcases.SectionCategoryAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.SectionCategories
{
    public class tblSectionCategoryTranslateConfiguration : IEntityTypeConfiguration<tblSectionCategoryTranslate>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblSectionCategoryTranslate> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.LangId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.SectionCategoryId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.FileId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Title).IsRequired(false).HasMaxLength(100);

            builder.HasOne(a => a.tblLanguages)
                   .WithMany(a => a.tblSectionCategoryTranslate)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LangId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblSectionCategories)
                   .WithMany(a => a.tblSectionCategoryTranslate)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.SectionCategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblFiles)
                   .WithMany(a => a.tblSectionCategoryTranslate)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.FileId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
