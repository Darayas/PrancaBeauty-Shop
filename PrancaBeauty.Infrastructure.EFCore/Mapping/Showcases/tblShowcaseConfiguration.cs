using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Domin.Settings.SettingsAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Entities;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Showcases
{
    public class tblShowcaseConfiguration : IEntityTypeConfiguration<tblShowcases>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblShowcases> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.UserId).IsRequired().HasMaxLength(450);
            builder.Property(a => a.CountryId).IsRequired(false).HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.CssStyle).IsRequired(false).HasMaxLength(500);
            builder.Property(a => a.CssClass).IsRequired(false).HasMaxLength(200);

            builder.HasOne(a => a.tblUser)
                   .WithMany(a => a.tblShowCases)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblCountry)
                   .WithMany(a => a.tblShowCases)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
