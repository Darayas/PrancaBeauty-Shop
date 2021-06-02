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
        Task<IdentityResult> AddPasswordAsync(tblUsers entity, string Password);
        Task<IdentityResult> AddPhoneNumberPasswordAsync(tblUsers entity, string Password);
        Task<IdentityResult> AddToRolesAsync(tblUsers user, string[] Roles);
        Task<IdentityResult> ChangeEmailAsync(tblUsers user, string newEmail, string token);
        Task<IdentityResult> CreateUserAsync(tblUsers User, string Password);
        Task<IdentityResult> DeleteAsync(tblUsers entity);
        Task<IdentityResult> EmailConfirmationAsync(tblUsers User, string Token);
        Task<tblUsers> FindByEmailAsync(string Email);
        Task<tblUsers> FindByIdAsync(string UserId);
        Task<tblUsers> FindByPhoneNumberAsync(string PhoneNumber);
        Task<string> GenerateChangeEmailTokenAsync(tblUsers user, string newEmail);
        Task<string> GenerateEmailConfirmationTokenAsync(tblUsers user);
        Task<string> GetUserIdByEmailAsync(string Email);
        Task<string> GetUserIdByPhoneNumberAsync(string PhoneNumber);
        Task<string> GetUserIdByUserNameAsync(string UserName);
        Task<bool> HasPasswordAsync(tblUsers user);
        Task<bool> IsEmailConfirmedAsync(tblUsers User);
        Task<SignInResult> PasswordSignInAsync(tblUsers user, string password, bool isPersistent, bool lockoutOnFailure);
        Task<IdentityResult> RemoveAllRolesAsync(tblUsers user);
        Task<IdentityResult> RemoveAsync(tblUsers user);
        Task<IdentityResult> RemovePasswordAsync(tblUsers entity);
        Task<IdentityResult> RemovePhoneNumberPasswordAsync(tblUsers entity);
        bool RequireConfirmedEmail();
    }
}
