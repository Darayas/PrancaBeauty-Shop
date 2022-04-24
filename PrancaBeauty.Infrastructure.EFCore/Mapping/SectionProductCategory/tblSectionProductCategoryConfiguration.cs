using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Showcases.SectionItems.Entitiy;
using PrancaBeauty.Domin.Showcases.SectionProductCategoryAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.SectionProductCategory
{
    public class tblSectionProductCategoryConfiguration : IEntityTypeConfiguration<tblSectionProductCategory>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblSectionProductCategory> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.TabSectionItemId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.CategoryId).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblShowcaseTabSectionItems)
                   .WithOne(a => a.tblSectionProductCategory)
                   .HasPrincipalKey<tblShowcaseTabSectionItems>(a => a.Id)
                   .HasForeignKey<tblSectionProductCategory>(a => a.TabSectionItemId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblCategory)
                   .WithMany(a => a.tblSectionProductCategory)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
