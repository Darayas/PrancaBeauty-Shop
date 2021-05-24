using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Region.CityAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Region.Cities
{
    public class tblCitiesConfiguration : IEntityTypeConfiguration<tblCities>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblCities> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProvinceId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(200);

            builder.HasOne(a => a.tblProvince)
                   .WithMany(a => a.tblCities)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProvinceId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
