using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Categories.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Category
{
    public class tblCategoy_TranslatesConfiguration : IEntityTypeConfiguration<tblCategory_Translates>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblCategory_Translates> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.CategoryId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.LangId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Description).IsRequired(false);

            builder.HasOne(a => a.tblCategoris)
                   .WithMany(a => a.tblCategory_Translates)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblLanguages)
                   .WithMany(a => a.tblCategory_Translates)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LangId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
