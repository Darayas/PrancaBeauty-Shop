using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Users.RoleAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Users.AccessLevelAgg.Entities
{
    public class tblAccessLevel_Roles : IEntity
    {
        public Guid Id { get; set; }
        public Guid AccessLevelId { get; set; }
        public Guid RoleId { get; set; }

        public virtual tblAccessLevels tblAccessLevels { get; set; }
        public virtual tblRoles tblRoles { get; set; }
    }
}
