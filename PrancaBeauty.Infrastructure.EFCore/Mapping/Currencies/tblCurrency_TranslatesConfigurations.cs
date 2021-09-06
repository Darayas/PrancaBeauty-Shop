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
    public class tblCurrency_TranslatesConfigurations : IEntityTypeConfiguration<tblCurrency_Translates>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblCurrency_Translates> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.CurrencyId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(250);

            builder.HasOne(a => a.tblCurrency)
                   .WithMany(a => a.tblCurrency_Translates)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CurrencyId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblLanguage)
                   .WithMany(a => a.tblCurrency_Translates)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LangId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
