using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Showcases.SectionItems.Entitiy;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.SectionItems
{
    public class tblShowcaseTabSectionItemsConfiguration : IEntityTypeConfiguration<tblShowcaseTabSectionItems>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblShowcaseTabSectionItems> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.TabSectionId).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblShowcaseTabSections)
                   .WithMany(a => a.tblShowcaseTabSectionItems)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.TabSectionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
