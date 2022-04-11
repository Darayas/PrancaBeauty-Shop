using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Sliders.SliderAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Slider
{
    public class tblSliderConfigurations : IEntityTypeConfiguration<tblSlider>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblSlider> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.UserId).IsRequired().HasMaxLength(450);
            builder.Property(a => a.FileId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Url).IsRequired().HasMaxLength(500);

            builder.HasOne(a => a.tblUsers)
                   .WithMany(a => a.tblSlider)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(a => a.tblFiles)
                   .WithMany(a => a.tblSlider)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.FileId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}