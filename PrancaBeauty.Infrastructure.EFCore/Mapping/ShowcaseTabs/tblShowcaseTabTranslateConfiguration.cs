using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.ShowcaseTabs
{
    public class tblShowcaseTabTranslateConfiguration : IEntityTypeConfiguration<tblShowcaseTabTranslates>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblShowcaseTabTranslates> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.LangId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ShowcaseTabId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(100);

            builder.HasOne(a => a.tblShowcaseTabs)
                   .WithMany(a => a.tblShowcaseTabTranslates)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ShowcaseTabId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblLanguages)
                   .WithMany(a => a.tblShowcaseTabTranslates)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LangId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
