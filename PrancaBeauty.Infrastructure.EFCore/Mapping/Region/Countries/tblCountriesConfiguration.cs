using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Region.Countries
{
    public class tblCountriesConfiguration : IEntityTypeConfiguration<tblCountries>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblCountries> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.FlagImgId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(150);
            builder.Property(a => a.PhoneCode).IsRequired().HasMaxLength(50);

            builder.HasOne(a => a.tblFiles)
                   .WithMany(a => a.tblCountries)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.FlagImgId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
