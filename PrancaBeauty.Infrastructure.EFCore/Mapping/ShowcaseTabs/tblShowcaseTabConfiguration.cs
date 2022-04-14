using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.ShowcaseTabs
{
    public class tblShowcaseTabConfiguration : IEntityTypeConfiguration<tblShowcaseTabs>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblShowcaseTabs> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ShowcaseId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(150);
            builder.Property(a => a.BackgroundColorCode).IsRequired(false).HasMaxLength(100);

            builder.HasOne(a => a.tblShowcases)
                   .WithMany(a => a.tblShowcaseTabs)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ShowcaseId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
