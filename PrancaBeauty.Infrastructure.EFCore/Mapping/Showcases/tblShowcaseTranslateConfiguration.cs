using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Showcases
{
    public class tblShowcaseTranslateConfiguration : IEntityTypeConfiguration<tblShowcasesTranslates>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblShowcasesTranslates> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.LangId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ShowcaseId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Title).IsRequired(false).HasMaxLength(150);
            builder.Property(a => a.Description).IsRequired(false).HasMaxLength(150);

            builder.HasOne(a => a.tblLanguage)
                   .WithMany(a => a.tblShowCasesTranslate)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LangId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblShowcase)
                   .WithMany(a => a.tblShowcasesTranslates)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ShowcaseId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
