using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Wallet.WalletDetailsAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.WalletDetails
{
    public class tblWalletDetailsConfiguration : IEntityTypeConfiguration<tblWalletDetails>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblWalletDetails> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.WalletId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Data).IsRequired().HasMaxLength(5000);

            builder.HasOne(a => a.tblWallets)
                .WithMany(a => a.tblWalletDetails)
                .HasPrincipalKey(a => a.Id)
                .HasForeignKey(a => a.WalletId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
