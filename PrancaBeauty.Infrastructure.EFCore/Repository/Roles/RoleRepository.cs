using Framework.Infrastructure;
using PrancaBeauty.Domin.Users.RoleAgg.Contracts;
using PrancaBeauty.Domin.Users.RoleAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Roles
{
    public class RoleRepository : BaseRepository<tblRoles>, IRoleRepository
    {
        public RoleRepository(MainContext Context) : base(Context)
        {

        }
    }
}
