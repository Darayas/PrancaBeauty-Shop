using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Showcases.SectionFreeItemAgg.Entities;
using PrancaBeauty.Domin.Showcases.SectionItems.Entitiy;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.SectionFreeItem
{
    public class tblSectionFreeItemConfiguration : IEntityTypeConfiguration<tblSectionFreeItems>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblSectionFreeItems> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.TabSectionItemId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);

            builder.HasOne(a => a.tblShowcaseTabSectionItems)
                   .WithOne(a => a.tblSectionFreeItems)
                   .HasPrincipalKey<tblShowcaseTabSectionItems>(a => a.Id)
                   .HasForeignKey<tblSectionFreeItems>(a => a.TabSectionItemId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
