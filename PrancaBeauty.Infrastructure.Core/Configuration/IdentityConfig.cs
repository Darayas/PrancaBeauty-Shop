using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PrancaBeauty.Domin.Users.RoleAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;

namespace PrancaBeauty.Infrastructure.Core.Configuration
{
    public static class IdentityConfig
    {
        public static IdentityBuilder AddCustomIdentity(this IServiceCollection services)
        {
            return services.AddIdentity<tblUsers, tblRoles>(opt =>
            {
                opt.SignIn.RequireConfirmedAccount = true;
                opt.SignIn.RequireConfirmedEmail = true;
                opt.SignIn.RequireConfirmedPhoneNumber = true;

                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
                opt.User.RequireUniqueEmail = true;

                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 15, 0);
                opt.Lockout.MaxFailedAccessAttempts = 3;
            })
            .AddEntityFrameworkStores<MainContext>()
            .AddDefaultTokenProviders();
        }
    }
}
