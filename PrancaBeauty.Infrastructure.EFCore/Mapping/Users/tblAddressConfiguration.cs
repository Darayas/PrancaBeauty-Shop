using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Users.AddressAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Users
{
    public class tblAddressConfiguration : IEntityTypeConfiguration<tblAddress>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblAddress> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.UserId).IsRequired().HasMaxLength(450);
            builder.Property(a => a.CountryId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProviceId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.CityId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.District).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Address).IsRequired().HasMaxLength(500);
            builder.Property(a => a.Plaque).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Unit).IsRequired(false).HasMaxLength(100);
            builder.Property(a => a.PostalCode).IsRequired().HasMaxLength(100);
            builder.Property(a => a.NationalCode).IsRequired().HasMaxLength(100);
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.PhoneNumber).IsRequired().HasMaxLength(100);

            builder.HasOne(a => a.tblUsers)
                   .WithMany(a => a.tblAddress)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblCountries)
                   .WithMany(a => a.tblAddress)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblProvinces)
                   .WithMany(a => a.tblAddress)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProviceId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblCities)
                   .WithMany(a => a.tblAddress)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CityId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
