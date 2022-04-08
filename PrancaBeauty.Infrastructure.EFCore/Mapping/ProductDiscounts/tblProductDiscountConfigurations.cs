using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Product.ProductDiscountAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.ProductDiscounts
{
    public class tblProductDiscountConfigurations : IEntityTypeConfiguration<tblProductDiscounts>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProductDiscounts> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.SellerId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(300);

            builder.HasOne(a => a.tblSellers)
                   .WithMany(a => a.tblProductDiscounts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.SellerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
