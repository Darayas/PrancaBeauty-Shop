using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Application.Contracts.Users;
using PrancaBeauty.Domin.Users.UserAgg.Contracts;
using PrancaBeauty.Infrastructure.Logger.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Users
{
    public class UserApplication: IUserApplication
    {
        private readonly ILogger _Logger;
        private readonly IUserRepository _UserRepository;

        public UserApplication(ILogger logger, IUserRepository userRepository)
        {
            _Logger = logger;
            _UserRepository = userRepository;
        }

        public async Task<OperationResult> AddUserAsync(InpAddUser Input)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }
    }
}
