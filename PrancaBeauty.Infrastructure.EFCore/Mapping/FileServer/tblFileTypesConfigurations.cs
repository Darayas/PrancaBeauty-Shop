﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.FileServer.FileTypeAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.FileServer
{
    public class tblFileTypesConfigurations : IEntityTypeConfiguration<tblFileTypes>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblFileTypes> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.IconUrl).IsRequired().HasMaxLength(300);
            builder.Property(a => a.MimeType).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Extentions).IsRequired().HasMaxLength(20);

        }
    }
}
