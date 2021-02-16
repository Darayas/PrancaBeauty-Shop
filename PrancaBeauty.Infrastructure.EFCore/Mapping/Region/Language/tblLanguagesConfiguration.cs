using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Region.Languages.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Region.Language
{
    public class tblLanguagesConfiguration : IEntityTypeConfiguration<tblLanguages>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblLanguages> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Code).IsRequired().HasMaxLength(20);
            builder.Property(a => a.NativeName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.NativeName).IsRequired().HasMaxLength(100);
        }
    }
}
