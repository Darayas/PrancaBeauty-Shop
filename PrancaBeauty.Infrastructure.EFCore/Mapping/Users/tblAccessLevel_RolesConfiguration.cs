using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Users
{
    public class tblAccessLevel_RolesConfiguration : IEntityTypeConfiguration<tblAccessLevel_Roles>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblAccessLevel_Roles> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(150);
            builder.Property(a => a.AccessLevelId).IsRequired().HasMaxLength(150);
            builder.Property(a => a.RoleId).IsRequired().HasMaxLength(450);

            builder.HasOne(a => a.tblAccessLevels)
                .WithMany(a => a.tblAccessLevel_Roles)
                .HasPrincipalKey(a => a.Id)
                .HasForeignKey(a => a.AccessLevelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.tblRoles)
                .WithMany(a => a.tblAccessLevel_Roles)
                .HasPrincipalKey(a => a.Id)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
