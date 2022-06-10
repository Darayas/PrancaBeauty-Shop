using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Wallet.WalletProductDepositDetailsAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.WalletProductDepositDetails
{
    public class tblWalletProductDepositDetailsConfiguration : IEntityTypeConfiguration<tblWalletProductDepositDetails>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblWalletProductDepositDetails> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.WalletDetailsId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.VariantItemId).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblWalletDetails)
                .WithMany(a => a.tblWalletProductDepositDetails)
                .HasPrincipalKey(a => a.Id)
                .HasForeignKey(a => a.WalletDetailsId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblProducts)
                .WithMany(a => a.tblWalletProductDepositDetails)
                .HasPrincipalKey(a => a.Id)
                .HasForeignKey(a => a.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblProductVariantItems)
                .WithMany(a => a.tblWalletProductDepositDetails)
                .HasPrincipalKey(a => a.Id)
                .HasForeignKey(a => a.VariantItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
