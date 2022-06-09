using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Product.ProductGroupAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.ProductGroupTranslate
{
    public class ProductGroupTranslateConfiguration : IEntityTypeConfiguration<tblProductGroupTranslate>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProductGroupTranslate> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.LangId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ProductGroupId).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(100);

            builder.HasOne(a => a.tblLanguages)
                   .WithMany(a => a.tblProductGroupTranslate)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.LangId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblProductGroups)
                   .WithMany(a => a.tblProductGroupTranslate)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.ProductGroupId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
