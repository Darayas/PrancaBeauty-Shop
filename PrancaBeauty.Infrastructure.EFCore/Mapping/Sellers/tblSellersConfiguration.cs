﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Users.AddressAgg.Entities;
using PrancaBeauty.Domin.Users.SellerAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Sellers
{
    public class tblSellersConfiguration : IEntityTypeConfiguration<tblSellers>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblSellers> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.UserId).IsRequired().HasMaxLength(450);
            builder.Property(a => a.AddressId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(200);

            builder.HasOne(a => a.tblUsers)
                .WithOne(a => a.tblSellers)
                .HasPrincipalKey<tblUsers>(a => a.Id)
                .HasForeignKey<tblSellers>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblAddress)
                .WithOne(a => a.tblSellers)
                .HasPrincipalKey<tblAddress>(a => a.Id)
                .HasForeignKey<tblSellers>(a => a.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
