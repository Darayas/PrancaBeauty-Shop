using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.BankAccount.BankAccountAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.BankAccounts
{
    public class tblBankAccountsConfiguration : IEntityTypeConfiguration<tblBankAccounts>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblBankAccounts> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.UserId).IsRequired().HasMaxLength(450);
            builder.Property(a => a.AccountNumber).IsRequired().HasMaxLength(100);
            builder.Property(a => a.CardNumber).IsRequired(false).HasMaxLength(100);
            builder.Property(a => a.IBAN).IsRequired().HasMaxLength(100);
            builder.Property(a => a.FullName).IsRequired().HasMaxLength(100);

            builder.HasOne(a => a.tblUsers)
                   .WithMany(a => a.tblBankAccounts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
