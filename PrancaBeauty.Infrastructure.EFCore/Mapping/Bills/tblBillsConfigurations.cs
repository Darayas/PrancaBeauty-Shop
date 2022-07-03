using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Bills.BillAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Bills
{
    public class tblBillsConfigurations : IEntityTypeConfiguration<tblBills>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblBills> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.UserId).IsRequired().HasMaxLength(450);
            builder.Property(a => a.GateId).IsRequired(false).HasMaxLength(150);
            builder.Property(a => a.AddressId).IsRequired(false).HasMaxLength(150);
            builder.Property(a => a.BillNumber).IsRequired().HasMaxLength(50);
            builder.Property(a => a.TransactionNumber).IsRequired(false).HasMaxLength(50);
            builder.Property(a => a.Authority).IsRequired(false).HasMaxLength(50);
            builder.Property(a => a.Note).IsRequired(false).HasMaxLength(500);

            builder.HasOne(a => a.tblUsers)
                   .WithMany(a => a.tblBills)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblPaymentGates)
                   .WithMany(a => a.tblBills)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.GateId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblAddress)
                   .WithMany(a => a.tblBills)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.AddressId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
