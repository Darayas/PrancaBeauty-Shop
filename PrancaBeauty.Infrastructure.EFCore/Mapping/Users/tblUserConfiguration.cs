using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Users
{
    public class tblUserConfiguration : IEntityTypeConfiguration<tblUsers>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblUsers> builder)
        {
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(100);

            builder.HasOne(a => a.tblAccessLevels)
                   .WithMany(a => a.tblUsers)
                   .HasPrincipalKey(a => a.Id)
                   .HasForeignKey(a => a.AccessLevelId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
