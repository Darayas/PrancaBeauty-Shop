using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.ShippingMethods
{
    public class tblShippingMethodsConfigurations : IEntityTypeConfiguration<tblShippingMethods>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblShippingMethods> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.LogoId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblFiles)
                   .WithMany(a => a.tblShippingMethods)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LogoId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
