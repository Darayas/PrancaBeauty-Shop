using PrancaBeauty.Application.Apps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Authentication
{
    public class JWTBuilder : IJWTBuilder
    {
        private readonly IUserApplication _UserApplication;
        public JWTBuilder(IUserApplication userApplication)
        {
            _UserApplication = userApplication;
        }

        public async Task<string> CreateTokenAync(string UserId)
        {
            var _UserDetails = await _UserApplication.GetAllUserDetailsAsync(UserId);
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,_UserDetails.UserName)
            };
        }
    }
}
