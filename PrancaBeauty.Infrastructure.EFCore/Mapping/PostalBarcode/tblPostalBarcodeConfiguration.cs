using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.PostalBarcodes.PostalBarcodeAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.PostalBarcode
{
    public class tblPostalBarcodeConfiguration : IEntityTypeConfiguration<tblPostalBarcodes>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblPostalBarcodes> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.BillId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ShippingMethodId).IsRequired(false).HasMaxLength(150);
            builder.Property(a => a.AddressId).IsRequired(false).HasMaxLength(150);
            builder.Property(a => a.Barcode).IsRequired().HasMaxLength(50);

            builder.HasOne(a => a.tblBill)
                   .WithMany(a => a.tblPostalBarcodes)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.BillId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblShippingMethod)
                   .WithMany(a => a.tblPostalBarcodes)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ShippingMethodId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblAddress)
                   .WithMany(a => a.tblPostalBarcodes)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.AddressId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
