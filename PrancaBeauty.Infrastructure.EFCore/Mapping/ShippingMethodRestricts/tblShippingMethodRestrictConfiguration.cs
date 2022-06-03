using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodRestrictAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.ShippingMethodRestricts
{
    public class tblShippingMethodRestrictConfiguration : IEntityTypeConfiguration<tblShippingMethodRestricts>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblShippingMethodRestricts> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ShippingMethodId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.CountryId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProvinceId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProvinceId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.CityId).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblShippingMethods)
                   .WithMany(a => a.tblShippingMethodRestricts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ShippingMethodId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblCountry)
                   .WithMany(a => a.tblShippingMethodRestricts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblProvinces)
                   .WithMany(a => a.tblShippingMethodRestricts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProvinceId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblCity)
                   .WithMany(a => a.tblShippingMethodRestricts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CityId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
