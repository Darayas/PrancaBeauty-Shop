using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Users.RoleAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using PrancaBeauty.Infrastructure.EFCore.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Users
{
    public class tblRolesConfiguration : IEntityTypeConfiguration<tblRoles>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblRoles> builder)
        {
            builder.Property(a => a.ParentId).IsRequired(false).HasMaxLength(450);
            builder.Property(a => a.PageName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Description).IsRequired().HasMaxLength(500);
        }
    }
}
