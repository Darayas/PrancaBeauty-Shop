using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.PaymentGates
{
    public class tblPaymentGateTranslateConfiguration : IEntityTypeConfiguration<tblPaymentGateTranslate>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblPaymentGateTranslate> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.LangId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.PaymentGateId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblLanguages)
                   .WithMany(a => a.tblPaymentGateTranslate)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LangId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblPaymentGates)
                  .WithMany(a => a.tblPaymentGateTranslate)
                  .HasPrincipalKey(a => a.Id)
                  .HasForeignKey(a => a.PaymentGateId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}