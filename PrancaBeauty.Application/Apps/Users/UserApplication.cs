using Framework.Application.Consts;
using Framework.Application.Services.Email;
using Framework.Application.Services.Sms;
using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.Accesslevels;
using PrancaBeauty.Application.Apps.Templates;
using PrancaBeauty.Application.Common.FtpWapper;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Application.Contracts.Users;
using PrancaBeauty.Domin.Users.UserAgg.Contracts;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Pipelines;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Users
{
    public class UserApplication : IUserApplication
    {
        private readonly ILogger _Logger;
        private readonly IEmailSender _EmailSender;
        private readonly ISmsSender _SmsSender;
        private readonly ILocalizer _Localizer;
        private readonly IUserRepository _UserRepository;
        private readonly IAccesslevelApplication _AccesslevelApplication;
        private readonly ITemplateApplication _TemplateApplication;
        private readonly IFtpWapper _FtpWapper;

        public UserApplication(ILogger logger, IUserRepository userRepository, IAccesslevelApplication accesslevelApplication, IEmailSender emailSender, ILocalizer localizer, ITemplateApplication templateApplication, ISmsSender smsSender, IFtpWapper ftpWapper)
        {
            _Logger = logger;
            _UserRepository = userRepository;
            _AccesslevelApplication = accesslevelApplication;
            _EmailSender = emailSender;
            _Localizer = localizer;
            _TemplateApplication = templateApplication;
            _SmsSender = smsSender;
            _FtpWapper = ftpWapper;
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
                    throw new ArgumentNullException("Paswword cant be null.");

                var userId = await _UserRepository.GetUserIdByUserNameAsync(UserName);
                if (userId == null)
                    return new OperationResult().Failed("UserNameOrPasswordIsInvalid");

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

                var ReNewPasswordResult = await ReCreatePasswordAsync(qUser);
                if (ReNewPasswordResult.IsSucceeded)
                {
                    return new OperationResult().Succeeded(qUser.Id + ", " + ReNewPasswordResult.Message + ", " + IP + ", " + DateTime.Now.ToString("yy/MM/dd HH:mm"));
                }
                else
                {
                    return new OperationResult().Failed(ReNewPasswordResult.Message);
                }
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

            if (string.IsNullOrWhiteSpace(UserId))
                throw new ArgumentNullException("UserId cant be null.");

            if (string.IsNullOrWhiteSpace(Password))
                throw new ArgumentNullException("Password cant be null.");

            var qUser = await _UserRepository.FindByIdAsync(UserId);

            if (qUser == null)
                return new OperationResult().Failed("LinkExipred");

            if (qUser.EmailConfirmed == false)
                return new OperationResult().Failed("LinkExipred");

            if (qUser.IsActive == false)
                return new OperationResult().Failed("YourAccountIsDisabled");

            if (qUser.PasswordPhoneNumber != Password.ToMD5())
                return new OperationResult().Failed("LinkExipred");

            return new OperationResult().Succeeded(qUser.Id.ToString());
        }

        public async Task<OperationResult> LoginByPhoneNumberStep1Async(string PhoneNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(PhoneNumber))
                    throw new ArgumentNullException("PhoneNumber cant be null");

                var qUser = await GetUserByPhoneNumberAsync(PhoneNumber);

                if (qUser == null)
                    return new OperationResult().Failed("PhoneNumberNotFound");

                if (qUser.PhoneNumberConfirmed == false)
                    return new OperationResult().Failed("PleaseConfirmYourPhoneNumber");

                if (qUser.IsActive == false)
                    return new OperationResult().Failed("YourAccountIsDisabled");

                if (qUser.LastTrySentSms.HasValue)
                    if (qUser.LastTrySentSms.Value.AddMinutes(AuthConst.LimitToResendSmsInMinute) > DateTime.Now)
                        return new OperationResult().Failed("LimitToResendSms2Minute");

                var ReNewPasswordResult = await ReCreatePasswordAsync(qUser);
                if (ReNewPasswordResult.IsSucceeded)
                {
                    return new OperationResult().Succeeded(ReNewPasswordResult.Message);
                }
                else
                {
                    return new OperationResult().Failed(ReNewPasswordResult.Message);
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> LoginByPhoneNumberStep2Async(string PhoneNumber, string Code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(PhoneNumber))
                    throw new ArgumentNullException("PhoneNumber cant be null");

                if (string.IsNullOrWhiteSpace(Code))
                    throw new ArgumentNullException("Code cant be null");

                var qUser = await GetUserByPhoneNumberAsync(PhoneNumber);

                if (qUser == null)
                    return new OperationResult().Failed("PhoneNumberNotFound");

                if (qUser.PhoneNumberConfirmed == false)
                    return new OperationResult().Failed("PleaseConfirmYourPhoneNumber");

                if (qUser.IsActive == false)
                    return new OperationResult().Failed("YourAccountIsDisabled");

                if (qUser.PasswordPhoneNumber != Code.ToMD5())
                    return new OperationResult().Failed("CodeIsInvalid");

                if (qUser.LastTrySentSms.HasValue)
                    if (qUser.LastTrySentSms.Value.AddMinutes(10) < DateTime.Now)
                        return new OperationResult().Failed("CodeIsExpired");

                return new OperationResult().Succeeded(qUser.Id.ToString());
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> ReCreatePasswordAsync(tblUsers User)
        {
            if (User.LastTrySentSms.HasValue)
                if (User.LastTrySentSms.Value.AddMinutes(AuthConst.LimitToResendSmsInMinute) > DateTime.Now)
                    return new OperationResult().Failed("LimitToResendSms2Minute");

            #region حذف پسورد قبلی کاربر
            var Result = await _UserRepository.RemovePhoneNumberPasswordAsync(User);
            if (!Result.Succeeded)
            {
                _Logger.Error(string.Join(", ", Result.Errors.Select(a => a.Description)));
                return new OperationResult().Failed("UserNotFound");
            }
            #endregion

            #region تنظیم پسورد جدید برای کاربر
            string NewPassword = new Random().Next(10000, 99999).ToString();
            var AddPassResult = await _UserRepository.AddPhoneNumberPasswordAsync(User, NewPassword);
            if (!AddPassResult.Succeeded)
            {
                _Logger.Error(string.Join(", ", AddPassResult.Errors.Select(a => a.Description)));
                return new OperationResult().Failed("UserNotFound");
            }
            #endregion

            return new OperationResult().Succeeded(NewPassword);
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

        public async Task<tblUsers> GetUserByPhoneNumberAsync(string PhoneNumber)
        {
            var qUser = await _UserRepository.FindByPhoneNumberAsync(PhoneNumber);
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

        public async Task<OperationResult> RemoveAllRolesAsync(tblUsers user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentInvalidException("user cant be null.");

                var Result = await _UserRepository.RemoveAllRolesAsync(user);
                if (Result.Succeeded)
                {
                    return new OperationResult().Succeeded();
                }
                else
                {
                    return new OperationResult().Failed(string.Join(" | ", Result.Errors.Select(a => new { ErrTxt = a.Code + "-" + a.Description })));
                }
            }
            catch (ArgumentInvalidException ex)
            {
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> AddRolesAsync(tblUsers user, string[] Roles)
        {
            try
            {
                if (user == null)
                    throw new ArgumentInvalidException("user cant be null.");

                if (Roles == null)
                    throw new ArgumentInvalidException("Roles cant be null.");

                var Result = await _UserRepository.AddToRolesAsync(user, Roles);
                if (Result.Succeeded)
                {
                    return new OperationResult().Succeeded();
                }
                else
                {
                    return new OperationResult().Failed(string.Join(" | ", Result.Errors.Select(a => new { ErrTxt = a.Code + "-" + a.Description })));
                }
            }
            catch (ArgumentInvalidException ex)
            {
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> EditUsersRoleByAccIdAsync(string AccessLevelId, string[] Roles)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(AccessLevelId))
                    throw new ArgumentInvalidException("AccessLevelId cant be null.");

                if (Roles == null)
                    throw new ArgumentInvalidException("Roles cant be null.");

                //دریافت لیست کاربران بر اساس سطح دسترسی
                var qUsers = await _UserRepository.Get.Where(a => a.AccessLevelId == Guid.Parse(AccessLevelId)).ToListAsync();

                foreach (var item in qUsers)
                {
                    // حذف عضویت رول ها
                    var RemoveResult = await RemoveAllRolesAsync(item);
                    if (RemoveResult.IsSucceeded)
                    {
                        // افزودن
                        var AddResult = await AddRolesAsync(item, Roles);
                        if (AddResult.IsSucceeded == false)
                            _Logger.Warning($"زمان افزودن عضویت رول ها برای کاربر با شناسه: [{item.Id}]، خطاهایی به شرح زیر رخ داد: [{RemoveResult.Message}]");
                    }
                    else
                    {
                        _Logger.Warning($"زمان حذف عضویت رول ها برای کاربر با شناسه: [{item.Id}]، خطاهایی به شرح زیر رخ داد: [{RemoveResult.Message}]");
                    }
                }
                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<List<string>> GetUserIdsByAccIdAsync(string AccessLevelId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(AccessLevelId))
                    throw new ArgumentInvalidException("AccessLevelId cant be null.");

                var qUsers = await _UserRepository.Get.Where(a => a.AccessLevelId == Guid.Parse(AccessLevelId)).Select(a => a.Id.ToString()).ToListAsync();

                return qUsers;
            }
            catch (ArgumentInvalidException)
            {
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<(OutPagingData, List<OutGetListForAdminPage>)> GetListForAdminPageAsync(string Email, string PhoneNumber, string FullName, string Sort, int PageNum, int Take)
        {
            try
            {
                if (PageNum < 1)
                    throw new ArgumentInvalidException("PageNum < 1");

                if (Take < 1)
                    throw new ArgumentInvalidException("Take < 1");

                Email = string.IsNullOrWhiteSpace(Email) ? null : Email;
                PhoneNumber = string.IsNullOrWhiteSpace(PhoneNumber) ? null : PhoneNumber;
                FullName = string.IsNullOrWhiteSpace(FullName) ? null : FullName;

                // آماده سازی اولیه ی کویری
                var qData = _UserRepository.Get
                    .Select(a => new OutGetListForAdminPage
                    {
                        Id = a.Id.ToString(),
                        AccessLevelId = a.AccessLevelId.ToString(),
                        FullName = a.FirstName + " " + a.LastName,
                        Email = a.Email,
                        PhoneNumber = a.PhoneNumber,
                        AccessLevelName = a.tblAccessLevels.Name,
                        Date = a.Date,
                        IsActive = a.IsActive,
                        IsEmailConfirmed = a.EmailConfirmed,
                        IsPhoneNumberConfirmed = a.PhoneNumberConfirmed
                    })
                .Where(a => FullName != null ? a.FullName.Contains(FullName) : true)
                .Where(a => Email != null ? a.Email.Contains(Email) : true)
                .Where(a => PhoneNumber != null ? a.PhoneNumber.Contains(PhoneNumber) : true)
                .OrderByDescending(a => a.Date);

                #region مرتب سازی
                if (Sort != null)
                    switch (Sort.ToLower())
                    {
                        case "fullnamedes":
                            {
                                qData = qData.OrderByDescending(a => a.FullName);
                                break;
                            }
                        case "fullnameaes":
                            {
                                qData = qData.OrderBy(a => a.FullName);
                                break;
                            }
                        case "emaildes":
                            {
                                qData = qData.OrderByDescending(a => a.Email);
                                break;
                            }
                        case "emailaes":
                            {
                                qData = qData.OrderBy(a => a.Email);
                                break;
                            }
                        case "isactivedes":
                            {
                                qData = qData.OrderByDescending(a => a.IsActive);
                                break;
                            }
                        case "isactiveaes":
                            {
                                qData = qData.OrderBy(a => a.IsActive);
                                break;
                            }
                        case "dateaes":
                            {
                                qData = qData.OrderByDescending(a => a.Date);
                                break;
                            }
                        case "datedes":
                            {
                                qData = qData.OrderBy(a => a.Date);
                                break;
                            }
                        case "confirmaccountdes":
                            {
                                qData = qData.OrderByDescending(a => a.IsEmailConfirmed);
                                break;
                            }
                        case "confirmaccountaes":
                            {
                                qData = qData.OrderBy(a => a.IsEmailConfirmed);
                                break;
                            }
                        case "confirmphonenumberdes":
                            {
                                qData = qData.OrderByDescending(a => a.IsPhoneNumberConfirmed);
                                break;
                            }
                        case "confirmphonenumberAes":
                            {
                                qData = qData.OrderBy(a => a.IsPhoneNumberConfirmed);
                                break;
                            }
                        default:
                            {
                                qData = qData.OrderByDescending(a => a.Date);
                                break;
                            }
                    }
                #endregion

                // صفحه بندی داده ها
                var qPagingData = PagingData.Calc(await qData.LongCountAsync(), PageNum, Take);

                return (qPagingData, await qData.Skip((int)qPagingData.Skip).Take(qPagingData.Take).ToListAsync());
            }
            catch (ArgumentInvalidException)
            {
                return (null, null);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return (null, null);
            }
        }

        public async Task<OperationResult> RemoveUserAsync(string UserId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserId))
                    throw new ArgumentInvalidException($"UserId cannot be null.");

                var qUser = await _UserRepository.FindByIdAsync(UserId);
                if (qUser == null)
                    return new OperationResult().Failed("UserNotFound");

                // برسی تایید نشده بودن کاربر
                if (qUser.EmailConfirmed || qUser.PhoneNumberConfirmed)
                    return new OperationResult().Failed("YouCantDeleteUser");

                var Result = await _UserRepository.RemoveAsync(qUser);
                if (Result.Succeeded)
                {
                    return new OperationResult().Succeeded("UserDeleted");
                }
                else
                {
                    return new OperationResult().Failed(string.Join(" | ", Result.Errors.Select(a => a.Description)));
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> ChangeUserStatusAsync(string UserId, string SelfUserId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserId))
                    throw new ArgumentInvalidException($"UserId cannot be null.");

                // جلوگیری از تغییر وضعیت حساب خود
                if (UserId == SelfUserId)
                    return new OperationResult().Failed("YouCantChanageYourAccountStatus");

                var qUser = await _UserRepository.FindByIdAsync(UserId);
                if (qUser == null)
                    return new OperationResult().Failed("UserNotFound");

                if (qUser.Email.ToLower() == "reza9025@gmail.com")
                    return new OperationResult().Failed("YouCantChanageAdminAccountStatus");

                qUser.IsActive = !qUser.IsActive;

                await _UserRepository.UpdateAsync(qUser, default, true);

                return new OperationResult().Succeeded("UserChangeStatus");
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> ChanageUserAccessLevelAsync(string UserId, string SelfUserId, string AccessLevelId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserId))
                {
                    throw new ArgumentInvalidException($"'{nameof(UserId)}' cannot be null.");
                }

                if (string.IsNullOrWhiteSpace(SelfUserId))
                {
                    throw new ArgumentInvalidException($"'{nameof(SelfUserId)}' cannot be null.");
                }

                if (string.IsNullOrWhiteSpace(AccessLevelId))
                {
                    throw new ArgumentInvalidException($"'{nameof(AccessLevelId)}' cannot be null.");
                }

                // جلوگیری از تغییر سطح دسترسی حساب خود
                if (UserId == SelfUserId)
                    return new OperationResult().Failed("YouCantChanageYourAccountAccessLevel");

                var qUser = await _UserRepository.FindByIdAsync(UserId);
                if (qUser == null)
                    return new OperationResult().Failed("UserNotFound");

                if (qUser.Email.ToLower() == "reza9025@gmail.com")
                    return new OperationResult().Failed("YouCantChanageAdminAccountAceessLevel");

                qUser.AccessLevelId = Guid.Parse(AccessLevelId);

                await _UserRepository.UpdateAsync(qUser, default, true);

                #region تغییر نقش های کاربر
                // حذف کلیه نقش های کاربر
                await RemoveAllRolesAsync(qUser);

                // واکشی نقش های موجود در سطح دسترسی
                var qRoles = await _AccesslevelApplication.GetRolesNameByAccIdAsync(qUser.AccessLevelId.ToString());

                // افزودن نقش های جدید به کاربر
                await AddRolesAsync(qUser, qRoles.ToArray());
                #endregion

                return new OperationResult().Succeeded("UserChangeAccessLevel");
            }
            catch (ArgumentInvalidException ex)
            {
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OutGetUserDetailsForAccountSettings> GetUserDetailsForAccountSettingsAsync(string UserId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserId))
                    throw new ArgumentInvalidException($"UserId cannot be null or whitespace.");

                var qData = await _UserRepository.Get
                                               .Where(a => a.Id == Guid.Parse(UserId))
                                               .Select(a => new OutGetUserDetailsForAccountSettings
                                               {
                                                   LangId = a.LangId.ToString(),
                                                   Email = a.Email,
                                                   PhoneNumber = a.PhoneNumber,
                                                   FirstName = a.FirstName,
                                                   LastName = a.LastName,
                                                   BirthDate = a.BirthDate,
                                                   PhoneNumberConfirmed = a.PhoneNumberConfirmed
                                               }).SingleOrDefaultAsync();

                //if (qData == null)
                //    return null;

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<OperationResult> SaveAccountSettingUserDetailsAsync(string UserId, InpSaveAccountSettingUserDetails Input, string UrlToChangeEmail)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserId))
                    throw new ArgumentInvalidException($"UserId cannot be null or whitespace.");

                if (Input is null)
                    throw new ArgumentInvalidException($"Input cannot be null.");

                var qUser = await _UserRepository.FindByIdAsync(UserId);

                if (qUser == null)
                    return new OperationResult().Failed("User Not Found.");

                bool FlgChangeEmail = false;
                bool FlgChangePhoneNumber = false;

                qUser.LangId = Guid.Parse(Input.LangId);
                qUser.FirstName = Input.FirstName;
                qUser.LastName = Input.LastName;
                qUser.BirthDate = Input.BirthDate;

                #region ویرایش تصویر پروفایل
                {
                    if (Input.ProfileImage != null)
                    {
                        if (qUser.ProfileImgId != null)
                        {
                            // حذف تصویر قبلی
                            await _FtpWapper.RemoveFileAsync(qUser.ProfileImgId.Value.ToString());
                            qUser.tblFiles = null;
                        }

                        // اپلود تصویر جدید
                        string _FileId = await _FtpWapper.UplaodCategoryImgAsync(Input.ProfileImage, qUser.FirstName + "-" + qUser.LastName);
                        qUser.ProfileImgId = Guid.Parse(_FileId);
                    }
                }
                #endregion

                #region تغییر شماره موبایل
                if (qUser.PhoneNumber != Input.PhoneNumber)
                {
                    qUser.PhoneNumber = Input.PhoneNumber;
                    qUser.PhoneNumberConfirmed = false;

                    FlgChangePhoneNumber = true;
                }
                #endregion

                await _UserRepository.UpdateAsync(qUser, default, true);

                #region تغییر ایمیل
                if (qUser.Email != Input.Email)
                {
                    string NewEmail = Input.Email;
                    string Token = await _UserRepository.GenerateChangeEmailTokenAsync(qUser, NewEmail);

                    string EncryptedData = $"{UserId}, {NewEmail}, {Token}".AesEncrypt(AuthConst.SecretKey);

                    string _Url = UrlToChangeEmail.Replace("[Token]", WebUtility.UrlEncode(EncryptedData));

                    await _EmailSender.SendAsync(NewEmail, _Localizer["ChangeEmailSubject"], await _TemplateApplication.GetEmailChangeTemplateAsync(CultureInfo.CurrentCulture.Name, _Url));

                    FlgChangeEmail = true;
                }
                #endregion

                // نمایش پیغام ها
                if (FlgChangeEmail && FlgChangePhoneNumber)
                    return new OperationResult().Succeeded("Operation was successded,ChangeEmail,ChangePhoneNumber");
                else if (FlgChangeEmail && !FlgChangePhoneNumber)
                    return new OperationResult().Succeeded("Operation was successded,ChangeEmail");
                else if (!FlgChangeEmail && FlgChangePhoneNumber)
                    return new OperationResult().Succeeded("Operation was successded,ChangePhoneNumber");
                else
                    return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> ChangeEmailAsync(string Token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Token))
                    throw new ArgumentInvalidException("'Token' cannot be null or whitespace.");

                string DecryptedToken = Token.AesDecrypt(AuthConst.SecretKey);

                string _UserId = DecryptedToken.Split(", ")[0];
                string _NewEmail = DecryptedToken.Split(", ")[1];
                string _Token = DecryptedToken.Split(", ")[2];

                var qUser = await _UserRepository.FindByIdAsync(_UserId);

                var Result = await _UserRepository.ChangeEmailAsync(qUser, _NewEmail, _Token);
                if (Result.Succeeded)
                {
                    qUser.UserName = _NewEmail;
                    qUser.NormalizedUserName = _NewEmail.ToUpper();
                    await _UserRepository.UpdateAsync(qUser, default, true);

                    return new OperationResult().Succeeded();
                }
                else
                {
                    return new OperationResult().Failed(string.Join(", ", Result.Errors.Select(a => a.Description)));
                }
            }
            catch (ArgumentInvalidException ex)
            {
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> ReSendSmsCodeAsync(string PhoneNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(PhoneNumber))
                    throw new ArgumentInvalidException($"PhoneNumber cannot be null or whitespace.");

                var qUser = await _UserRepository.FindByPhoneNumberAsync(PhoneNumber);
                if (qUser == null)
                    return new OperationResult().Failed("PhoneNumberNotFound");

                if (qUser.IsActive == false)
                    return new OperationResult().Failed("YourAccountIsDisabled");

                if (qUser.LastTrySentSms.HasValue)
                    if (qUser.LastTrySentSms.Value.AddMinutes(AuthConst.LimitToResendSmsInMinute) > DateTime.Now)
                        return new OperationResult().Failed("LimitToResendSms2Minute");

                var ReNewPasswordResult = await ReCreatePasswordAsync(qUser);
                if (ReNewPasswordResult.IsSucceeded)
                {
                    var IsSend = _SmsSender.SendLoginCode(PhoneNumber, ReNewPasswordResult.Message);
                    if (IsSend)
                        return new OperationResult().Succeeded("SmsCodeIsSent");
                    else
                        return new OperationResult().Failed("SmsSenderNotRespond");
                }
                else
                {
                    return new OperationResult().Failed(ReNewPasswordResult.Message);
                }


            }
            catch (ArgumentInvalidException ex)
            {
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> PhoneConfirmationBySmsCodeAsync(string UserId, string PhoneNumber, string Code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserId))
                    throw new ArgumentInvalidException($"'UserId' cannot be null or whitespace.");

                if (string.IsNullOrWhiteSpace(PhoneNumber))
                    throw new ArgumentInvalidException($"'PhoneNumber' cannot be null or whitespace.");

                if (string.IsNullOrWhiteSpace(Code))
                    throw new ArgumentInvalidException($"'Code' cannot be null or whitespace.");

                var qUser = await _UserRepository.FindByIdAsync(UserId);
                if (qUser == null)
                    return new OperationResult().Failed("UserIdNotFound");

                if (qUser.PhoneNumber != PhoneNumber)
                    return new OperationResult().Failed("PhoneNumberNotFound");

                if (qUser.PhoneNumberConfirmed)
                    return new OperationResult().Failed("PhoneNumberNotFound");

                if (qUser.PasswordPhoneNumber != Code.ToMD5())
                    return new OperationResult().Failed("CodeIsInvalid");

                qUser.PhoneNumberConfirmed = true;

                await _UserRepository.UpdateAsync(qUser, default, true);

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> ChanagePasswordAsync(string UserId, string CurrentPassword, string NewPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserId))
                    throw new ArgumentInvalidException($"'{nameof(UserId)}' cannot be null or whitespace.");

                if (string.IsNullOrWhiteSpace(CurrentPassword))
                    throw new ArgumentInvalidException($"'{nameof(CurrentPassword)}' cannot be null or whitespace.");

                if (string.IsNullOrWhiteSpace(NewPassword))
                    throw new ArgumentInvalidException($"'{nameof(NewPassword)}' cannot be null or whitespace.");

                var qUser = await _UserRepository.FindByIdAsync(UserId);
                if (qUser == null)
                    return new OperationResult().Failed("UserNotFound");

                var Result = await _UserRepository.ChangePasswordAsync(qUser, CurrentPassword, NewPassword);
                if (Result.Succeeded)
                {
                    return new OperationResult().Succeeded();
                }
                else
                {
                    return new OperationResult().Failed(string.Join(", ", Result.Errors.Select(a => a.Description)));
                }
            }
            catch (ArgumentInvalidException ex)
            {
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed(ex.Message);

            }
        }

        public async Task<List<OutGetListForCombo>> GetListForComboAsync(string LangId, string Name)
        {
            try
            {
                var qData = await _UserRepository.Get
                                                 .Where(a => LangId != null ? a.LangId == Guid.Parse(LangId) : true)
                                                 .Select(a => new OutGetListForCombo
                                                 {
                                                     Id = a.Id.ToString(),
                                                     FullName = a.FirstName + " " + a.LastName,
                                                     ImgUrl = null
                                                 })
                                                 .Where(a => Name != null ? a.FullName.Contains(Name) : true)
                                                 .OrderBy(a => a.FullName)
                                                 .ToListAsync();

                return qData;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
