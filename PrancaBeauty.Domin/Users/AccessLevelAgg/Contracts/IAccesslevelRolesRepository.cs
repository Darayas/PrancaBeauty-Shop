using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Users.AccessLevelAgg.Contracts
{
    public interface IAccesslevelRolesRepository : IRepository<tblAccessLevel_Roles>
    {
    }
}
