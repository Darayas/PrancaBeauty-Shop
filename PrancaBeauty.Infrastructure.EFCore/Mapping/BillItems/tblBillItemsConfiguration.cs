using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Bills.BillItemsAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.BillItems
{
    public class tblBillItemsConfiguration : IEntityTypeConfiguration<tblBillItems>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblBillItems> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.BillId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.VarianrItemId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.SellerId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.PostalBarcodeId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ReasonReturn).IsRequired(false).HasMaxLength(500);

            builder.HasOne(a => a.tblBills)
                   .WithMany(a => a.tblBillItems)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.BillId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblProducts)
                   .WithMany(a => a.tblBillItems)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblProductVariantItems)
                   .WithMany(a => a.tblBillItems)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.VarianrItemId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblSellers)
                   .WithMany(a => a.tblBillItems)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.SellerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblPostalBarcodes)
                   .WithMany(a => a.tblBillItems)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.PostalBarcodeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}