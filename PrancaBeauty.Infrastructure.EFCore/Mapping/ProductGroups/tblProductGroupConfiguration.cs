using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Product.ProductGroupAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.ProductGroups
{
    public class tblProductGroupConfiguration : IEntityTypeConfiguration<tblProductGroups>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblProductGroups> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.TopicId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);

            builder.HasOne(a => a.tblProductTopic)
                   .WithMany(a => a.tblProductGroups)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.TopicId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
