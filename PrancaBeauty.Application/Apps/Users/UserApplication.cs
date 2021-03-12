using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.Accesslevels;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Application.Contracts.Users;
using PrancaBeauty.Domin.Users.UserAgg.Contracts;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Users
{
    public class UserApplication : IUserApplication
    {
        private readonly ILogger _Logger;
        private readonly IUserRepository _UserRepository;
        private readonly IAccesslevelApplication _AccesslevelApplication;

        public UserApplication(ILogger logger, IUserRepository userRepository, IAccesslevelApplication accesslevelApplication)
        {
            _Logger = logger;
            _UserRepository = userRepository;
            _AccesslevelApplication = accesslevelApplication;
        }

        public async Task<OperationResult> AddUserAsync(InpAddUser Input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Input.Email))
                    throw new ArgumentNullException("Email cant be null.");

                if (string.IsNullOrWhiteSpace(Input.PhoneNumber))
                    throw new ArgumentNullException("PhoneNumber cant be null.");

                if (string.IsNullOrWhiteSpace(Input.FirstName))
                    throw new ArgumentNullException("FirstName cant be null.");

                if (string.IsNullOrWhiteSpace(Input.LastName))
                    throw new ArgumentNullException("LastName cant be null.");

                if (string.IsNullOrWhiteSpace(Input.Password))
                    throw new ArgumentNullException("Password cant be null.");

                tblUsers tUser = new tblUsers()
                {
                    Date = DateTime.Now,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    AccessLevelId = Guid.Parse(await _AccesslevelApplication.GetIdByNameAsync("Users")),
                    IsActive = true,
                    PhoneNumber = Input.PhoneNumber,
                    UserName = Input.Email
                };

                var Result = await _UserRepository.CreateUserAsync(tUser, Input.Password);
                if (Result.Succeeded)
                {
                    if (_UserRepository.RequireConfirmedEmail())
                        return new OperationResult().Succeeded(1, tUser.Id.ToString());
                    else
                        return new OperationResult().Succeeded("UserCreatedSuccessfully");
                }
                else
                {
                    return new OperationResult().Failed(string.Join(", ", Result.Errors.Select(a => a.Description)));
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string UserId)
        {
            var qUser = await _UserRepository.FindByIdAsync(UserId);

            return await _UserRepository.GenerateEmailConfirmationTokenAsync(qUser);
        }

        public async Task<OperationResult> EmailConfirmationAsync(string UserId, string Token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserId))
                    throw new ArgumentNullException("UserId cant be null.");

                if (string.IsNullOrWhiteSpace(Token))
                    throw new ArgumentNullException("Token cant be null.");

                if (await IsEmailConfirmedAsync(UserId))
                    return new OperationResult().Failed("EmailAlreadyVerified");

                var qUser = await _UserRepository.FindByIdAsync(UserId);

                var Result = await _UserRepository.EmailConfirmationAsync(qUser, Token);
                if (Result.Succeeded)
                {
                    return new OperationResult().Succeeded("EmailConfirmationSuccesfully");
                }
                else
                {
                    return new OperationResult().Failed(string.Join(", ", Result.Errors.Select(a => a.Description)));
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> LoginByUserNamePasswordAsync(string UserName, string Password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserName))
                    throw new ArgumentNullException("UserName cant be null.");

                if (string.IsNullOrWhiteSpace(Password))
                    throw new ArgumentNullException("Pawword cant be null.");

                var userId = await _UserRepository.GetUserIdByUserNameAsync(UserName);

                return await LoginAsync(userId, Password);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> LoginByEmailLinkStep1Async(string Email, string IP)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Email))
                    throw new ArgumentNullException("Email cant be null.");

                var qUser = await GetUserByEmailAsync(Email);

                if (qUser == null)
                    return new OperationResult().Failed("EmailNotFound");

                if (qUser.EmailConfirmed == false)
                    return new OperationResult().Failed("PleaseConfirmYourEmail");

                if (qUser.IsActive == false)
                    return new OperationResult().Failed("YourAccountIsDisabled");

                #region حذف پسورد قبلی کاربر
                if (await _UserRepository.HasPasswordAsync(qUser))
                {
                    var Result = await _UserRepository.RemovePasswordAsync(qUser);
                    if (!Result.Succeeded)
                    {
                        _Logger.Error(string.Join(", ", Result.Errors.Select(a => a.Description)));
                        return new OperationResult().Failed("EmailNotFound");
                    }
                }
                #endregion

                #region تنظیم پسورد جدید برای کاربر
                string NewPassword = new Random().Next(100000, 999999).ToString();
                var AddPassResult = await _UserRepository.AddPasswordAsync(qUser, NewPassword);
                if (!AddPassResult.Succeeded)
                {
                    _Logger.Error(string.Join(", ", AddPassResult.Errors.Select(a => a.Description)));
                    return new OperationResult().Failed("EmailNotFound");
                }
                #endregion

                return new OperationResult().Succeeded(qUser.Id + ", " + NewPassword + ", " + IP + ", " + DateTime.Now.ToString("yy/MM/dd HH:mm"));
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> LoginByEmailLinkStep2Async(string UserId, string Password, string LinkIP, string UserIP, DateTime Date)
        {
            if (LinkIP != UserIP)
                return new OperationResult().Failed("LinkExipred");

            if (Date.AddMinutes(60) < DateTime.Now)
                return new OperationResult().Failed("LinkExipred");

            return await LoginAsync(UserId, Password);
        }

        public async Task<OperationResult> LoginAsync(string UserId, string Password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserId))
                    throw new ArgumentNullException("UserId cant be null.");

                if (string.IsNullOrWhiteSpace(Password))
                    throw new ArgumentNullException("Password cant be null.");

                var qUser = await _UserRepository.FindByIdAsync(UserId);

                if (qUser == null)
                    return new OperationResult().Failed("UserNameOrPasswordIsInvalid");

                if (qUser.EmailConfirmed == false)
                    return new OperationResult().Failed("PleaseConfirmYourEmail");

                if (qUser.IsActive == false)
                    return new OperationResult().Failed("YourAccountIsDisabled");

                var Result = await _UserRepository.PasswordSignInAsync(qUser, Password, true, true);
                if (Result.Succeeded)
                {
                    return new OperationResult().Succeeded(qUser.Id.ToString());
                }
                else
                {
                    if (Result.IsLockedOut)
                        return new OperationResult().Failed("UserIsLockedOut");
                    else if (Result.IsNotAllowed)
                        return new OperationResult().Failed("UserIsLockedOut");
                    else
                        return new OperationResult().Failed("UserNameOrPasswordIsInvalid");
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<bool> IsEmailConfirmedAsync(string UserId)
        {
            var qUser = await _UserRepository.FindByIdAsync(UserId);

            return await _UserRepository.IsEmailConfirmedAsync(qUser);
        }

        public async Task<OutGetAllUserDetails> GetAllUserDetailsAsync(string UserId)
        {
            try
            {
                var qData = await _UserRepository.Get
                                        .Where(a => a.Id == Guid.Parse(UserId))
                                        .Select(a => new OutGetAllUserDetails
                                        {
                                            Id = a.Id.ToString(),
                                            UserName = a.UserName,
                                            Email = a.Email,
                                            PhoneNumber = a.PhoneNumber,
                                            AccessLevelId = a.AccessLevelId.ToString(),
                                            AccessLevelTitle = a.tblAccessLevels.Name,
                                            FirstName = a.FirstName,
                                            LastName = a.LastName,
                                            Date = a.Date,
                                            IsActive = a.IsActive
                                        })
                                        .SingleOrDefaultAsync();

                if (qData == null)
                    return null;

                return qData;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<tblUsers> GetUserByIdAsync(string UserId)
        {
            try
            {
                return await _UserRepository.FindByIdAsync(UserId);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<tblUsers> GetUserByEmailAsync(string Email)
        {
            var qUser = await _UserRepository.FindByEmailAsync(Email);
            return qUser;
        }

        public async Task<bool> RemoveUnConfirmedUserAsync(string Email)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Email))
                    throw new ArgumentNullException("UserId cant be null.");

                var qUser = await GetUserByEmailAsync(Email);
                if (qUser == null)
                    return true;

                if (qUser.EmailConfirmed)
                    return true;

                var Result = await _UserRepository.DeleteAsync(qUser);
                if (Result.Succeeded)
                    return true;
                else
                    throw new Exception(string.Join(", ", Result.Errors.Select(a => a.Code + "-" + a.Description)));
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }


    }
}
