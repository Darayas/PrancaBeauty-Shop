using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.PaymentGateRestricts
{
    public class tblShippingMethodTranslateConfigurations : IEntityTypeConfiguration<tblShippingMethodTranslate>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblShippingMethodTranslate> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.LangId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ShippingMethodId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblShippingMethods)
                   .WithMany(a => a.tblShippingMethodTranslate)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ShippingMethodId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblLanguages)
                   .WithMany(a => a.tblShippingMethodTranslate)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LangId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
