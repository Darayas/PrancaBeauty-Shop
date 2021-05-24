using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Region.CityAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Region.Cities
{
    public class tblCities_TranslatesConfiguration : IEntityTypeConfiguration<tblCities_Translates>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblCities_Translates> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.LangId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.CityId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(200);

            builder.HasOne(a => a.tblCity)
                   .WithMany(a => a.tblCities_Translates)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CityId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblLanguage)
                   .WithMany(a => a.tblCities_Translates)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LangId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
