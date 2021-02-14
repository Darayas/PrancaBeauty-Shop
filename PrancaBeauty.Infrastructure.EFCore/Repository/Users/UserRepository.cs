using Framework.Infrastructure;
using Microsoft.AspNetCore.Identity;
using PrancaBeauty.Domin.Users.UserAgg.Contracts;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Users
{
    public class UserRepository : BaseRepository<tblUsers>, IUserRepository
    {
        private readonly UserManager<tblUsers> _UserManager;
        public UserRepository(MainContext Context, UserManager<tblUsers> UserManager) : base(Context)
        {
            _UserManager = UserManager;
        }

        public async Task<IdentityResult> CreateUserAsync(tblUsers User,string Password)
        {
            return await _UserManager.CreateAsync(User, Password);
        }


    }
}
