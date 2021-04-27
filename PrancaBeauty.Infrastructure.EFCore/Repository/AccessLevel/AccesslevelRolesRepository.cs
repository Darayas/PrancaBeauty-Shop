using Framework.Infrastructure;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Contracts;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.AccessLevel
{
    public class AccesslevelRolesRepository : BaseRepository<tblAccessLevel_Roles>, IAccesslevelRolesRepository
    {
        public AccesslevelRolesRepository(MainContext Context) : base(Context)
        {

        }
    }
}
