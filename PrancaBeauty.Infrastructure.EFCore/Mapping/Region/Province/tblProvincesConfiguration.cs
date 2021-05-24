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
    public class tblProvincesConfiguration : IEntityTypeConfiguration<tblProvinces>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProvinces> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.CountryId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(200);

            builder.HasOne(a => a.tblCountry)
                   .WithMany(a => a.tblProvinces)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
