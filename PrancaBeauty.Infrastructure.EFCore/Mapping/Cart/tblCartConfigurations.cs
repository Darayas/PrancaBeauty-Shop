using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Cart.CartAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Cart
{
    public class tblCartConfigurations : IEntityTypeConfiguration<tblCarts>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblCarts> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.UserId).IsRequired().HasMaxLength(450);
            builder.Property(a => a.ProductId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.SellerId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.VariantItemId).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblUsers)
                   .WithMany(a => a.tblCarts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblProducts)
                   .WithMany(a => a.tblCarts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblSellers)
                   .WithMany(a => a.tblCarts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.SellerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblProductVariantItems)
                   .WithMany(a => a.tblCarts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.VariantItemId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
