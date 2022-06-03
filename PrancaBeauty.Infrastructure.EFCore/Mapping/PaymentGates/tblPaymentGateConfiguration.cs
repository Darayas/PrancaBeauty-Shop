using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.PaymentGates
{
    public class tblPaymentGateConfiguration : IEntityTypeConfiguration<tblPaymentGates>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblPaymentGates> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.LogoId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Data).IsRequired().HasMaxLength(5000);

            builder.HasOne(a => a.tblFiles)
                   .WithMany(a => a.tblPaymentGates)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LogoId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
