using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.ShowcaseTabSection
{
    public class tblShowcaseTabSectionsConfigurtion : IEntityTypeConfiguration<tblShowcaseTabSections>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblShowcaseTabSections> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ParentId).IsRequired(false).HasMaxLength(150);
            builder.Property(a => a.ShowcaseTabId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);

            builder.HasOne(a => a.tblShowcaseTabSectionsParent)
                   .WithMany(a => a.tblShowcaseTabSectionsChild)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ParentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblShowcaseTabs)
                   .WithMany(a => a.tblShowcaseTabSections)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ShowcaseTabId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
