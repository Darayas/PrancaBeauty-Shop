using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.PaymentGate.PaymentGateRestrictAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.PaymentGateRestricts
{
    public class tblPaymentGateRestrictsConfigurations : IEntityTypeConfiguration<tblPaymentGateRestricts>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblPaymentGateRestricts> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.PaymentGateId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.CountryId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.CurrencyId).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblPaymentGates)
                   .WithMany(a => a.tblPaymentGateRestricts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.PaymentGateId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblCountry)
                   .WithMany(a => a.tblPaymentGateRestricts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblCurrency)
                   .WithMany(a => a.tblPaymentGateRestricts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CurrencyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
