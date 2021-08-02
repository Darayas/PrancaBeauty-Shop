using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Settings.SettingsAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Settings
{
    public class tblSettingsConfiguration : IEntityTypeConfiguration<tblSettings>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblSettings> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.LangId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.SiteTitle).IsRequired().HasMaxLength(100);
            builder.Property(a => a.SiteDescription).IsRequired().HasMaxLength(300);
            builder.Property(a => a.SiteEmail).IsRequired().HasMaxLength(100);
            builder.Property(a => a.SitePhoneNumber).IsRequired().HasMaxLength(50);

            builder.HasOne(a => a.tblLanguages)
                   .WithMany(a => a.tblSettings)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LangId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
