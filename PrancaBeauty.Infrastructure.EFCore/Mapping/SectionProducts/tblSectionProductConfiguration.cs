using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Showcases.SectionProductAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.SectionProducts
{
    public class tblSectionProductConfiguration : IEntityTypeConfiguration<tblSectionProducts>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblSectionProducts> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ShowcaseTabSectionId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductId).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblShowcaseTabSections)
                   .WithMany(a => a.tblSectionProducts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ShowcaseTabSectionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblProducts)
                   .WithMany(a => a.tblSectionProducts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
