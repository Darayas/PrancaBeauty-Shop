using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Bills.TaxAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Tax
{
    public class tblTaxConfiguration : IEntityTypeConfiguration<tblTaxGroups>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblTaxGroups> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.CountryId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Description).IsRequired(false).HasMaxLength(500);

            builder.HasOne(a => a.tblCountry)
                   .WithMany(a => a.tblTaxGroups)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}