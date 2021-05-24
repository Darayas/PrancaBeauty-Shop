using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Region.ProvinceAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Region.Province
{
    public class tblProvinces_TranslateConfiguration : IEntityTypeConfiguration<tblProvinces_Translate>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProvinces_Translate> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProvinceId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.LangId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(200);

            builder.HasOne(a => a.tblProvince)
                   .WithMany(a => a.tblProvinces_Translate)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProvinceId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblLanguage)
                   .WithMany(a => a.tblProvinces_Translate)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LangId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
