using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly SignInManager<tblUsers> _SignInManager;
        private readonly UserManager<tblUsers> _UserManager;
        public UserRepository(MainContext Context, UserManager<tblUsers> UserManager, SignInManager<tblUsers> signInManager) : base(Context)
        {
            _UserManager = UserManager;
            _SignInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(tblUsers User, string Password)
        {
            return await _UserManager.CreateAsync(User, Password);
        }

        public async Task<tblUsers> FindByIdAsync(string UserId)
        {
            return await _UserManager.FindByIdAsync(UserId);
        }

        public async Task<tblUsers> FindByEmailAsync(string Email)
        {
            return await _UserManager.FindByEmailAsync(Email);
        }

        public async Task<tblUsers> FindByPhoneNumberAsync(string PhoneNumber)
        {
            return await Get.Where(a => a.PhoneNumber == PhoneNumber).SingleOrDefaultAsync();
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(tblUsers user)
        {
            return await _UserManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public bool RequireConfirmedEmail()
        {
            return _UserManager.Options.SignIn.RequireConfirmedEmail;
        }

        public async Task<IdentityResult> EmailConfirmationAsync(tblUsers User, string Token)
        {
            if (User == null)
                throw new ArgumentNullException("User cant be null.");

            if (string.IsNullOrWhiteSpace(Token))
                throw new ArgumentNullException("Token cant be null.");

            return await _UserManager.ConfirmEmailAsync(User, Token);
        }

        public async Task<bool> IsEmailConfirmedAsync(tblUsers User)
        {
            return await _UserManager.IsEmailConfirmedAsync(User);
        }

        public async Task<SignInResult> PasswordSignInAsync(tblUsers user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await _SignInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }

        public async Task<string> GetUserIdByUserNameAsync(string UserName)
        {
            return await GetNoTraking.Where(a => a.UserName == UserName).Select(a => a.Id.ToString()).SingleOrDefaultAsync();
        }

        public async Task<string> GetUserIdByEmailAsync(string Email)
        {
            return await GetNoTraking.Where(a => a.Email == Email).Select(a => a.Id.ToString()).SingleOrDefaultAsync();
        }

        public async Task<string> GetUserIdByPhoneNumberAsync(string PhoneNumber)
        {
            return await GetNoTraking.Where(a => a.PhoneNumber == PhoneNumber).Select(a => a.Id.ToString()).SingleOrDefaultAsync();
        }

        public async Task<IdentityResult> DeleteAsync(tblUsers entity)
        {
            return await _UserManager.DeleteAsync(entity);
        }

        public async Task<bool> HasPasswordAsync(tblUsers user)
        {
            return await _UserManager.HasPasswordAsync(user);
        }

        public async Task<IdentityResult> RemovePasswordAsync(tblUsers entity)
        {
            return await _UserManager.RemovePasswordAsync(entity);
        }

        public async Task<IdentityResult> AddPasswordAsync(tblUsers entity, string Password)
        {
            return await _UserManager.AddPasswordAsync(entity, Password);
        }

        public async Task<IdentityResult> RemovePhoneNumberPasswordAsync(tblUsers entity)
        {
            entity.PasswordPhoneNumber = null;
            entity.LastTrySentSms = null;

            return await _UserManager.UpdateAsync(entity);
        }

        public async Task<IdentityResult> AddPhoneNumberPasswordAsync(tblUsers entity, string Password)
        {
            entity.PasswordPhoneNumber = Password.ToMD5();
            entity.LastTrySentSms = DateTime.Now;

            return await _UserManager.UpdateAsync(entity);
        }

        public async Task<IdentityResult> RemoveAllRolesAsync(tblUsers user)
        {
            // واکشی رول های فعلی کاربر
            var qRoles = await _UserManager.GetRolesAsync(user);

            // حذف عضویت
            return await _UserManager.RemoveFromRolesAsync(user, qRoles);
        }

        public async Task<IdentityResult> AddToRolesAsync(tblUsers user, string[] Roles)
        {
            // افزودن عضویت
            return await _UserManager.AddToRolesAsync(user, Roles);
        }
    }
}
