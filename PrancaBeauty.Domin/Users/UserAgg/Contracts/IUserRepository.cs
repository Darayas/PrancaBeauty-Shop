using Framework.Domain;
using Microsoft.AspNetCore.Identity;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Users.UserAgg.Contracts
{
    public interface IUserRepository : IRepository<tblUsers>
    {
        Task<IdentityResult> CreateUserAsync(tblUsers User, string Password);
    }
}
