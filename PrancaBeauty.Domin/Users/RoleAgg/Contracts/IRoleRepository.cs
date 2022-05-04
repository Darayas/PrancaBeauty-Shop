using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Users.RoleAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Users.RoleAgg.Contracts
{
    public interface IRoleRepository : IRepository<tblRoles>
    {
    }
}
