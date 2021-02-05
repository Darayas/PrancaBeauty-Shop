using Framework.Infrastructure;
using PrancaBeauty.Domin.Users.UserAgg.Contracts;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Users
{
    public class UserRepository : BaseRepository<tblUsers>, IUserRepository
    {
        public UserRepository(MainContext Context) : base(Context)
        {

        }


    }
}
