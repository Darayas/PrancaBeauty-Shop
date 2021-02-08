using Framework.Domain;
using Microsoft.AspNetCore.Identity;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrancaBeauty.Domin.Users.RoleAgg.Entities
{
    public class tblRoles : IdentityRole<Guid>, IEntity
    {
        public Guid? ParentId { get; set; }
        public string PageName { get; set; }
        public int Sort { get; set; }
        public string Description { get; set; }


        public virtual ICollection<tblAccessLevel_Roles> tblAccessLevel_Roles { get; set; }
    }
}
