using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Domin.Wallet.WalletDetailsAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrancaBeauty.Domin.Wallet.WalletWithdrawAgg.Entities;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.WalletWithdraw
{
    public class tblWalletWithdrawConfiguration : IEntityTypeConfiguration<tblWalletWithdraw>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblWalletWithdraw> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.WalletDetailsId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.BankAccountId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Data).IsRequired();

            builder.HasOne(a => a.tblWalletDetails)
               .WithMany(a => a.tblWalletWithdraw)
               .HasPrincipalKey(a => a.Id)
               .HasForeignKey(a => a.WalletDetailsId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblBankAccounts)
               .WithMany(a => a.tblWalletWithdraw)
               .HasPrincipalKey(a => a.Id)
               .HasForeignKey(a => a.BankAccountId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
