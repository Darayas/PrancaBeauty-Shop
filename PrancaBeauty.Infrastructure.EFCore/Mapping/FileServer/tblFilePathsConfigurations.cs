using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.FileServer
{
    public class tblFilePathsConfigurations : IEntityTypeConfiguration<tblFilePaths>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblFilePaths> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.FileServerId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Path).IsRequired().HasMaxLength(300);

            builder.HasOne(a => a.tblFileServer)
                   .WithMany(a => a.tblFilePaths)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.FileServerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
