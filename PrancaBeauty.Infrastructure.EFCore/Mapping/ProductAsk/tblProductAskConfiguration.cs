using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Product.ProductAskAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.ProductAsk
{
    public class tblProductAskConfiguration : IEntityTypeConfiguration<tblProductAsk>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProductAsk> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.AskId).IsRequired(false).HasMaxLength(150);
            builder.Property(a => a.ProductId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.UserId).IsRequired().HasMaxLength(450);
            builder.Property(a => a.Text).IsRequired().HasMaxLength(1000);

            builder.HasOne(a => a.tblProductAsk_Parent)
                   .WithMany(a => a.tblProductAsk_Childs)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.AskId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblProducts)
                   .WithMany(a => a.tblProductAsk)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblUsers)
                   .WithMany(a => a.tblProductAsk)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
