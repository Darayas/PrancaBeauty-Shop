using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Users.RoleAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Common.ExMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Seed
{
    public class SeedRoles
    {
        public void Run(EntityTypeBuilder<tblRoles> builder)
        {
            builder.HasData(new tblRoles()
            {
                Id = new Guid().SequentialGuid(),
                ParentId = null,
                PageName = "FullControl",
                Sort = 0,
                Name = "FullControl",
                NormalizedName = "FullControl".ToUpper(),
                Description = "دسترسی مدیر کل"
            });

        }
    }
}
