using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Product
{
    public class tblProductConfigurations : IEntityTypeConfiguration<tblProducts>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProducts> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.UserId).IsRequired().HasMaxLength(450);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(250);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(250);
            builder.Property(a => a.Descreption).IsRequired();

            builder.HasOne(a => a.tblUsers)
                   .WithMany(a => a.tblProducts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblCategoris)
                   .WithMany(a => a.tblProducts)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
