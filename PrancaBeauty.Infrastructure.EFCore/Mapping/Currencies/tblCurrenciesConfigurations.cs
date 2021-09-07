using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Region.CurrnencyAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Currencies
{
    public class tblCurrenciesConfigurations : IEntityTypeConfiguration<tblCurrencies>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblCurrencies> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.CountryId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Symbol).IsRequired().HasMaxLength(50);
            builder.Property(a => a.aDec).IsRequired().HasMaxLength(5);
            builder.Property(a => a.aSep).IsRequired().HasMaxLength(5);
            builder.Property(a => a.mDec).IsRequired().HasMaxLength(5);
            builder.Property(a => a.vMax).IsRequired().HasMaxLength(15);

            builder.HasOne(a => a.tblCountry)
                   .WithMany(a => a.tblCurrencies)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CountryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
