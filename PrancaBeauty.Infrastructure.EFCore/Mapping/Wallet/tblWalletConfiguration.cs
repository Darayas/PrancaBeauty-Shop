using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Wallet.WalletAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Wallet
{
    public class tblWalletConfiguration : IEntityTypeConfiguration<tblWallets>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblWallets> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.UserId).IsRequired().HasMaxLength(450);
            builder.Property(a => a.CurrencyId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Data).IsRequired().HasMaxLength(5000);

            builder.HasOne(a => a.tblUsers)
                .WithMany(a => a.tblWallets)
                .HasPrincipalKey(a => a.Id)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblCurrencies)
                .WithMany(a => a.tblWallets)
                .HasPrincipalKey(a => a.Id)
                .HasForeignKey(a => a.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
