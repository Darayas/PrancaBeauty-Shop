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
        Task<IdentityResult> EmailConfirmationAsync(tblUsers User, string Token);
        Task<tblUsers> FindByIdAsync(string UserId);
        Task<string> GenerateEmailConfirmationTokenAsync(tblUsers user);
        Task<string> GetUserIdByEmailAsync(string Email);
        Task<string> GetUserIdByPhoneNumberAsync(string PhoneNumber);
        Task<string> GetUserIdByUserNameAsync(string UserName);
        Task<bool> IsEmailConfirmedAsync(tblUsers User);
        Task<SignInResult> PasswordSignInAsync(tblUsers user, string password, bool isPersistent, bool lockoutOnFailure);
        bool RequireConfirmedEmail();
    }
}
