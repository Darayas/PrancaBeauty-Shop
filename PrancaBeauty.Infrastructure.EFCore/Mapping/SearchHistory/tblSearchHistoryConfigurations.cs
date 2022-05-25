using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Keywords.SearchHistoryAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.SearchHistory
{
    public class tblSearchHistoryConfigurations : IEntityTypeConfiguration<tblSearchHistory>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblSearchHistory> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.LangId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(150);

            builder.HasOne(a => a.tblLanguages)
                  .WithMany(a => a.tblSearchHistory)
                  .HasPrincipalKey(a => a.Id)
                  .HasForeignKey(a => a.LangId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}