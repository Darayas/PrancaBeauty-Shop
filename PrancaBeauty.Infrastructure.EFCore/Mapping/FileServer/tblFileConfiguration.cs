using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.FileServer
{
    public class tblFileConfiguration : IEntityTypeConfiguration<tblFiles>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblFiles> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.FileServerId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.FileTypeId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.UserId).IsRequired(false).HasMaxLength(450);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Path).IsRequired().HasMaxLength(250);
            builder.Property(a => a.FileName).IsRequired().HasMaxLength(200);

            builder.HasOne(a => a.tblFileServer)
                   .WithMany(a => a.tblFiles)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.FileServerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblUser)
                   .WithMany(a => a.tblFiles)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.tblFileTypes)
                   .WithMany(a => a.tblFiles)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.FileTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
