using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Users.SellerAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Sellers
{
    public class tblSeller_TranslatesConfiguration : IEntityTypeConfiguration<tblSeller_Translates>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblSeller_Translates> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.LangId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.SellerId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.LogoId).IsRequired(false).HasMaxLength(150);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(250);

            builder.HasOne(a => a.tblLanguages)
                   .WithMany(a => a.tblSeller_Translates)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LangId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblSellers)
                   .WithMany(a => a.tblSeller_Translates)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.SellerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblFiles)
                   .WithMany(a => a.tblSeller_Translates)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LogoId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
