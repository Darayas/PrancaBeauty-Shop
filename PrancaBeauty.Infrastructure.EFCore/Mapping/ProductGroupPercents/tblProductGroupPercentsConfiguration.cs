using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Product.ProductGroupPercentAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.ProductGroupPercents
{
    public class tblProductGroupPercentsConfiguration : IEntityTypeConfiguration<tblProductGroupPercents>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProductGroupPercents> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductGroupId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.WalletId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(100);

            builder.HasOne(a => a.tblProductGroups)
                   .WithMany(a => a.tblProductGroupPercents)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductGroupId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblWallets)
                   .WithMany(a => a.tblProductGroupPercents)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.WalletId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
