using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Category
{
    public class tblCategoiesConfiguration : IEntityTypeConfiguration<tblCategoris>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblCategoris> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ParentId).IsRequired(false).HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired(false).HasMaxLength(100);
            builder.Property(a => a.ImageId).IsRequired(false).HasMaxLength(150);

            builder.HasOne(a => a.tblCategory_Parent)
                   .WithMany(a => a.tblCategory_Childs)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ParentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblFiles)
                   .WithMany(a => a.tblCategoris)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ImageId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
