using Framework.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrancaBeauty.Domin.Users.RoleAgg.Entities
{
    public class tblRoles : IdentityRole<Guid>, IEntity
    {

    }
}
