using PrancaBeauty.WebApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Common.ExMethod
{
    public static class IdentityEx
    {
        public static vmGetUserDetails GetUserDetails(this ClaimsPrincipal user)
        {
            //if (!user.Identity.IsAuthenticated)
            //    throw new Exception();

            var UserData = new vmGetUserDetails()
            {
                UserId = user.Claims.Where(a => a.Type == "nameidentifier").Select(a => a.Value).SingleOrDefault() ?? "",
                UserName = user.Claims.Where(a => a.Type == "name").Select(a => a.Value).SingleOrDefault() ?? "",
                Email = user.Claims.Where(a => a.Type == "email").Select(a => a.Value).SingleOrDefault() ?? "",
                MobileNumber = user.Claims.Where(a => a.Type == "mobilephone").Select(a => a.Value).SingleOrDefault() ?? "",
                GivenName = user.Claims.Where(a => a.Type == "givenname").Select(a => a.Value).SingleOrDefault() ?? "",
                Surname = user.Claims.Where(a => a.Type == "surname").Select(a => a.Value).SingleOrDefault() ?? "",
                AccessLevel = user.Claims.Where(a => a.Type == "AccessLevel").Select(a => a.Value).SingleOrDefault() ?? "",
                Date = DateTime.Parse(user.Claims.Where(a => a.Type == "Date").Select(a => a.Value).SingleOrDefault() ?? DateTime.MinValue.ToString()),
            };

            return UserData;
        }
    }
}
