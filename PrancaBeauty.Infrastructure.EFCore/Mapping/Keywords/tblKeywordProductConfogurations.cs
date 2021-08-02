using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Keywords.Keywords_Products.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Keywords
{
    public class tblKeywordProductConfogurations : IEntityTypeConfiguration<tblKeywords_Products>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblKeywords_Products> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.KeywordId).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblKeywords)
                   .WithMany(a => a.tblKeywords_Products)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.KeywordId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblProducts)
                   .WithMany(a => a.tblKeywords_Products)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
