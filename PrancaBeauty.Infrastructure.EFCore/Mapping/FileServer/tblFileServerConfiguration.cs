using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.FileServer.ServerAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.FileServer
{
    class tblFileServerConfiguration : IEntityTypeConfiguration<tblFileServers>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblFileServers> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Description).IsRequired(false);
            builder.Property(a => a.HttpDomin).IsRequired().HasMaxLength(200);
            builder.Property(a => a.HttpPath).IsRequired().HasMaxLength(250);
            builder.Property(a => a.FtpData).IsRequired();

        }
    }
}
