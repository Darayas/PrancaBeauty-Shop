﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Keywords.KeywordAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Keywords
{
    public class tblKeywordConfiguration : IEntityTypeConfiguration<tblKeywords>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblKeywords> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(250);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(250);
            builder.Property(a => a.Description);
        }
    }
}
