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
using PrancaBeauty.Application.Contracts.ApplicationDTO.AccessLevels;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Common.FtpWapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Templates;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Users;
using PrancaBeauty.Domin.Users.UserAgg.Contracts;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Users
{
    public class UserApplication : IUserApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IEmailSender _EmailSender;
        private readonly ISmsSender _SmsSender;
        private readonly ILocalizer _Localizer;
        private readonly IUserRepository _UserRepository;
        private readonly IAccesslevelApplication _AccesslevelApplication;
        private readonly ITemplateApplication _TemplateApplication;
        private readonly IFtpWapper _FtpWapper;

        public UserApplication(ILogger logger, IUserRepository userRepository, IAccesslevelApplication accesslevelApplication, IEmailSender emailSender, ILocalizer localizer, ITemplateApplication templateApplication, ISmsSender smsSender, IFtpWapper ftpWapper, IServiceProvider serviceProvider)
        {
            _Logger = logger;
            _UserRepository = userRepository;
            _AccesslevelApplication = accesslevelApplication;
            _EmailSender = emailSender;
            _Localizer = localizer;
            _TemplateApplication = templateApplication;
            _SmsSender = smsSender;
            _FtpWapper = ftpWapper;
            _ServiceProvider = serviceProvider;
        }

        public async Task<OperationResult> AddUserAsync(InpAddUser Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                await RemoveUnConfirmedUserAsync(Input.Email);

                var tUser = new tblUsers()
                {
                    Date = DateTime.Now,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    AccessLevelId = Guid.Parse(await _AccesslevelApplication.GetIdByNameAsync(new InpGetIdByName { Name = "Users" })),
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
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(InpGenerateEmailConfirmationToken Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                var qUser = await _UserRepository.FindByIdAsync(Input.UserId);

                return await _UserRepository.GenerateEmailConfirmationTokenAsync(qUser);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<OperationResult> EmailConfirmationAsync(InpEmailConfirmation Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                if (await IsEmailConfirmedAsync(Input.UserId))
                    return new OperationResult().Failed("EmailAlreadyVerified");

                var qUser = await _UserRepository.FindByIdAsync(Input.UserId);

                var Result = await _UserRepository.EmailConfirmationAsync(qUser, Input.Token);
                if (Result.Succeeded)
                {
                    return new OperationResult().Succeeded("EmailConfirmationSuccesfully");
                }
                else
                {
                    return new OperationResult().Failed(string.Join(", ", Result.Errors.Select(a => a.Description)));
                }
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> LoginByEmailPasswordAsync(InpLoginByEmailPassword Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion


                var userId = await _UserRepository.GetUserIdByEmailAsync(Input.Email);
                if (userId == null)
                    return new OperationResult().Failed("EmailOrPasswordIsInvalid");

                return await LoginAsync(new InpLogin { UserId = userId, Password = Input.Password });
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> LoginByUserNamePasswordAsync(InpLoginByUserNamePassword Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                var userId = await _UserRepository.GetUserIdByUserNameAsync(Input.UserName);
                if (userId == null)
                    return new OperationResult().Failed("UserNameOrPasswordIsInvalid");

                return await LoginAsync(new InpLogin { UserId = userId, Password = Input.Password });
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> LoginByEmailLinkStep1Async(InpLoginByEmailLinkStep1 Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                var qUser = await GetUserByEmailAsync(Input.Email);

                if (qUser == null)
                    return new OperationResult().Failed("EmailNotFound");

                if (qUser.EmailConfirmed == false)
                    return new OperationResult().Failed("PleaseConfirmYourEmail");

                if (qUser.IsActive == false)
                    return new OperationResult().Failed("YourAccountIsDisabled");

                var ReNewPasswordResult = await ReCreatePasswordAsync(new InpReCreatePassword { UserId = qUser.Id.ToString() });
                if (ReNewPasswordResult.IsSucceeded)
                {
                    return new OperationResult().Succeeded(qUser.Id + ", " + ReNewPasswordResult.Message + ", " + Input.IP + ", " + DateTime.Now.ToString("yy/MM/dd HH:mm"));
                }
                else
                {
                    return new OperationResult().Failed(ReNewPasswordResult.Message);
                }
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> LoginByEmailLinkStep2Async(InpLoginByEmailLinkStep2 Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                var qUser = await _UserRepository.FindByIdAsync(Input.UserId);

                if (qUser == null)
                    return new OperationResult().Failed("LinkExipred");

                if (qUser.EmailConfirmed == false)
                    return new OperationResult().Failed("LinkExipred");

                if (qUser.IsActive == false)
                    return new OperationResult().Failed("YourAccountIsDisabled");

                if (qUser.PasswordPhoneNumber != Input.Password.ToMD5())
                    return new OperationResult().Failed("LinkExipred");

                return new OperationResult().Succeeded(qUser.Id.ToString());
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> LoginByPhoneNumberStep1Async(InpLoginByPhoneNumberStep1 Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                //if (string.IsNullOrWhiteSpace(PhoneNumber))
                //    throw new ArgumentNullException("PhoneNumber cant be null");

                var qUser = await GetUserByPhoneNumberAsync(new InpGetUserByPhoneNumber { PhoneNumber = Input.PhoneNumber });

                if (qUser == null)
                    return new OperationResult().Failed("PhoneNumberNotFound");

                if (qUser.PhoneNumberConfirmed == false)
                    return new OperationResult().Failed("PleaseConfirmYourPhoneNumber");

                if (qUser.IsActive == false)
                    return new OperationResult().Failed("YourAccountIsDisabled");

                if (qUser.LastTrySentSms.HasValue)
                    if (qUser.LastTrySentSms.Value.AddMinutes(AuthConst.LimitToResendSmsInMinute) > DateTime.Now)
                        return new OperationResult().Failed("LimitToResendSms2Minute");

                var ReNewPasswordResult = await ReCreatePasswordAsync(new InpReCreatePassword { UserId = qUser.Id.ToString() });
                if (ReNewPasswordResult.IsSucceeded)
                {
                    return new OperationResult().Succeeded(ReNewPasswordResult.Message);
                }
                else
                {
                    return new OperationResult().Failed(ReNewPasswordResult.Message);
                }
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> LoginByPhoneNumberStep2Async(InpLoginByPhoneNumberStep2 Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                var qUser = await GetUserByPhoneNumberAsync(new InpGetUserByPhoneNumber { PhoneNumber = Input.PhoneNumber });

                if (qUser == null)
                    return new OperationResult().Failed("PhoneNumberNotFound");

                if (qUser.PhoneNumberConfirmed == false)
                    return new OperationResult().Failed("PleaseConfirmYourPhoneNumber");

                if (qUser.IsActive == false)
                    return new OperationResult().Failed("YourAccountIsDisabled");

                if (qUser.PasswordPhoneNumber != Input.Code.ToMD5())
                    return new OperationResult().Failed("CodeIsInvalid");

                if (qUser.LastTrySentSms.HasValue)
                    if (qUser.LastTrySentSms.Value.AddMinutes(10) < DateTime.Now)
                        return new OperationResult().Failed("CodeIsExpired");

                return new OperationResult().Succeeded(qUser.Id.ToString());
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> ReCreatePasswordAsync(InpReCreatePassword Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                var qUser = await _UserRepository.GetById(default, Input.UserId);
                if (qUser == null)
                    return new OperationResult().Failed("User not found");

                if (qUser.LastTrySentSms.HasValue)
                    if (qUser.LastTrySentSms.Value.AddMinutes(AuthConst.LimitToResendSmsInMinute) > DateTime.Now)
                        return new OperationResult().Failed("LimitToResendSms2Minute");

                #region حذف پسورد قبلی کاربر
                var Result = await _UserRepository.RemovePhoneNumberPasswordAsync(qUser);
                if (!Result.Succeeded)
                {
                    _Logger.Error(string.Join(", ", Result.Errors.Select(a => a.Description)));
                    return new OperationResult().Failed("UserNotFound");
                }
                #endregion

                #region تنظیم پسورد جدید برای کاربر
                string NewPassword = new Random().Next(10000, 99999).ToString();
                var AddPassResult = await _UserRepository.AddPhoneNumberPasswordAsync(qUser, NewPassword);
                if (!AddPassResult.Succeeded)
                {
                    _Logger.Error(string.Join(", ", AddPassResult.Errors.Select(a => a.Description)));
                    return new OperationResult().Failed("UserNotFound");
                }
                #endregion

                return new OperationResult().Succeeded(NewPassword);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> LoginAsync(InpLogin Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                //if (string.IsNullOrWhiteSpace(UserId))
                //    throw new ArgumentNullException("UserId cant be null.");

                //if (string.IsNullOrWhiteSpace(Password))
                //    throw new ArgumentNullException("Password cant be null.");

                var qUser = await _UserRepository.FindByIdAsync(Input.UserId);

                if (qUser == null)
                    return new OperationResult().Failed("UserNameOrPasswordIsInvalid");

                if (qUser.EmailConfirmed == false)
                    return new OperationResult().Failed("PleaseConfirmYourEmail");

                if (qUser.IsActive == false)
                    return new OperationResult().Failed("YourAccountIsDisabled");

                var Result = await _UserRepository.PasswordSignInAsync(qUser, Input.Password, true, true);
                if (Result.Succeeded)
                {
                    return new OperationResult().Succeeded(qUser.Id.ToString());
                }
                else
                {
                    if (Result.IsLockedOut)
                        return new OperationResult().Failed("UserIsLockedOut");
                    else if (Result.IsNotAllowed)
                        return new OperationResult().Failed("UserNameOrPasswordIsInvalid");
                    else
                        return new OperationResult().Failed("UserNameOrPasswordIsInvalid");
                }
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        private async Task<bool> IsEmailConfirmedAsync(string UserId)
        {

            var qUser = await _UserRepository.FindByIdAsync(UserId);

            return await _UserRepository.IsEmailConfirmedAsync(qUser);
        }

        public async Task<OutGetAllUserDetails> GetAllUserDetailsAsync(InpGetAllUserDetails Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _UserRepository.Get
                                        .Where(a => a.Id == Guid.Parse(Input.UserId))
                                        .Select(a => new OutGetAllUserDetails
                                        {
                                            Id = a.Id.ToString(),
                                            SellerId=a.tblSellers!=null?a.tblSellers.Id.ToString():null,
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
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<tblUsers> GetUserByIdAsync(InpGetUserById Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                return await _UserRepository.FindByIdAsync(Input.UserId);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        private async Task<tblUsers> GetUserByEmailAsync(string Email)
        {
            var qUser = await _UserRepository.FindByEmailAsync(Email);
            return qUser;
        }

        public async Task<tblUsers> GetUserByPhoneNumberAsync(InpGetUserByPhoneNumber Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                var qUser = await _UserRepository.FindByPhoneNumberAsync(Input.PhoneNumber);
                return qUser;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        private async Task<bool> RemoveUnConfirmedUserAsync(string Email)
        {
            var qUser = await GetUserByEmailAsync(Email);
            if (qUser == null)
                return true;

            if (qUser.EmailConfirmed)
                return true;

            var Result = await _UserRepository.DeleteAsync(qUser);
            if (Result.Succeeded)
                return true;
            else
                throw new Exception(string.Join(", ", Result.Errors.Select(a => a.Description)));
        }

        private async Task<OperationResult> RemoveAllRolesByUserIdAsync(InpRemoveAllRolesByUserId Input)
        {
            #region Validations
             Input.CheckModelState(_ServiceProvider);
            #endregion

            var qUser = await _UserRepository.GetById(default, Guid.Parse(Input.UserId));
            if (qUser == null)
                return new OperationResult().Failed("User not found.");

            var Result = await _UserRepository.RemoveAllRolesAsync(qUser);
            if (Result.Succeeded)
            {
                return new OperationResult().Succeeded();
            }
            else
            {
                return new OperationResult().Failed(string.Join(" | ", Result.Errors.Select(a => new { ErrTxt = a.Code + "-" + a.Description })));
            }

        }

        public async Task<OperationResult> AddRolesAsync(InpAddRoles Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);

                if (Input.Roles == null)
                    throw new ArgumentInvalidException("Input.Roles cant be null.");
                #endregion

                var qUser = await _UserRepository.GetById(default, Guid.Parse(Input.UserId));
                if (qUser == null)
                    return new OperationResult().Failed("User not found");


                var Result = await _UserRepository.AddToRolesAsync(qUser, Input.Roles.Select(a => a.Name).ToArray());
                if (Result.Succeeded)
                {
                    return new OperationResult().Succeeded();
                }
                else
                {
                    return new OperationResult().Failed(string.Join(",", Result.Errors.Select(a => new { ErrTxt = a.Code + "-" + a.Description })));
                }
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> EditUsersRoleByAccIdAsync(InpEditUsersRoleByAccId Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);

                if (Input.Roles == null)
                    throw new ArgumentInvalidException("Input.Roles cant be null.");
                #endregion

                //دریافت لیست کاربران بر اساس سطح دسترسی
                var qUsers = await _UserRepository.Get.Where(a => a.AccessLevelId == Guid.Parse(Input.AccessLevelId)).ToListAsync();

                foreach (var item in qUsers)
                {
                    // حذف عضویت رول ها
                    var RemoveResult = await RemoveAllRolesByUserIdAsync(new InpRemoveAllRolesByUserId { UserId = item.Id.ToString() });
                    if (RemoveResult.IsSucceeded)
                    {
                        // افزودن
                        var AddResult = await AddRolesAsync(new InpAddRoles { UserId = item.Id.ToString(), Roles = Input.Roles.Select(a => new InpAddRolesItem { Name = a.Name }).ToList() });
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
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<List<string>> GetUserIdsByAccIdAsync(InpGetUserIdsByAccId Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                //if (string.IsNullOrWhiteSpace(AccessLevelId))
                //    throw new ArgumentInvalidException("AccessLevelId cant be null.");

                var qUsers = await _UserRepository.Get.Where(a => a.AccessLevelId == Guid.Parse(Input.AccessLevelId)).Select(a => a.Id.ToString()).ToListAsync();

                return qUsers;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<(OutPagingData, List<Contracts.ApplicationDTO.Users.OutGetListForAdminPage>)> GetListForAdminPageAsync(Contracts.ApplicationDTO.Users.InpGetListForAdminPage Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                Input.Email = string.IsNullOrWhiteSpace(Input.Email) ? null : Input.Email;
                Input.PhoneNumber = string.IsNullOrWhiteSpace(Input.PhoneNumber) ? null : Input.PhoneNumber;
                Input.FullName = string.IsNullOrWhiteSpace(Input.FullName) ? null : Input.FullName;

                // آماده سازی اولیه ی کویری
                var qData = _UserRepository.Get
                    .Select(a => new Contracts.ApplicationDTO.Users.OutGetListForAdminPage
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
                        IsPhoneNumberConfirmed = a.PhoneNumberConfirmed,
                        ImgUrl = a.ProfileImgId != null ?
                                    a.tblProfileImage.tblFilePaths.tblFileServer.HttpDomin
                                    + a.tblProfileImage.tblFilePaths.tblFileServer.HttpPath
                                    + a.tblProfileImage.tblFilePaths.Path
                                    + a.tblProfileImage.FileName
                                : PublicConst.DefaultUserProfileImg
                    })
                .Where(a => Input.FullName != null ? a.FullName.Contains(Input.FullName) : true)
                .Where(a => Input.Email != null ? a.Email.Contains(Input.Email) : true)
                .Where(a => Input.PhoneNumber != null ? a.PhoneNumber.Contains(Input.PhoneNumber) : true)
                .OrderByDescending(a => a.Date);

                #region مرتب سازی
                if (Input.Sort != null)
                    switch (Input.Sort.ToLower())
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
                var qPagingData = PagingData.Calc(await qData.LongCountAsync(), Input.PageNum, Input.Take);

                return (qPagingData, await qData.Skip((int)qPagingData.Skip).Take(qPagingData.Take).ToListAsync());
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return (null, null);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return (null, null);
            }
        }

        public async Task<OperationResult> RemoveUserAsync(InpRemoveUser Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                var qUser = await _UserRepository.FindByIdAsync(Input.UserId);
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
                    return new OperationResult().Failed(string.Join(",", Result.Errors.Select(a => a.Description)));
                }
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> ChangeUserStatusAsync(InpChangeUserStatus Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                // جلوگیری از تغییر وضعیت حساب خود
                if (Input.UserId == Input.SelfUserId)
                    return new OperationResult().Failed("YouCantChanageYourAccountStatus");

                var qUser = await _UserRepository.FindByIdAsync(Input.UserId);
                if (qUser == null)
                    return new OperationResult().Failed("UserNotFound");

                if (qUser.Email.ToLower() == "reza9025@gmail.com")
                    return new OperationResult().Failed("YouCantChanageAdminAccountStatus");

                qUser.IsActive = !qUser.IsActive;

                await _UserRepository.UpdateAsync(qUser, default, true);

                return new OperationResult().Succeeded("UserChangeStatus");
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> ChanageUserAccessLevelAsync(InpChanageUserAccessLevel Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                // جلوگیری از تغییر سطح دسترسی حساب خود
                if (Input.UserId == Input.SelfUserId)
                    return new OperationResult().Failed("YouCantChanageYourAccountAccessLevel");

                var qUser = await _UserRepository.FindByIdAsync(Input.UserId);
                if (qUser == null)
                    return new OperationResult().Failed("UserNotFound");

                if (qUser.Email.ToLower() == "reza9025@gmail.com")
                    return new OperationResult().Failed("YouCantChanageAdminAccountAceessLevel");

                qUser.AccessLevelId = Guid.Parse(Input.AccessLevelId);

                await _UserRepository.UpdateAsync(qUser, default, true);

                #region تغییر نقش های کاربر
                // حذف کلیه نقش های کاربر
                await RemoveAllRolesByUserIdAsync(new InpRemoveAllRolesByUserId { UserId = Input.UserId });

                // واکشی نقش های موجود در سطح دسترسی
                var qRoles = await _AccesslevelApplication.GetRolesNameByAccIdAsync(new InpGetRolesNameByAccId() { AccessLevelId = qUser.AccessLevelId.ToString() });

                // افزودن نقش های جدید به کاربر
                await AddRolesAsync(new InpAddRoles { UserId = qUser.Id.ToString(), Roles = qRoles.Select(a => new InpAddRolesItem { Name = a }).ToList() });
                #endregion

                return new OperationResult().Succeeded("UserChangeAccessLevel");
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OutGetUserDetailsForAccountSettings> GetUserDetailsForAccountSettingsAsync(InpGetUserDetailsForAccountSettings Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion


                var qData = await _UserRepository.Get
                                                   .Where(a => a.Id == Guid.Parse(Input.UserId))
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

                if (qData == null)
                    return null;

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<OperationResult> SaveAccountSettingUserDetailsAsync(InpSaveAccountSettingUserDetails Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                var qUser = await _UserRepository.FindByIdAsync(Input.UserId);

                if (qUser == null)
                    return new OperationResult().Failed("User Not Found.");

                bool FlgChangeEmail = false;
                bool FlgChangePhoneNumber = false;

                qUser.LangId = Guid.Parse(Input.LangId);
                qUser.FirstName = Input.FirstName;
                qUser.LastName = Input.LastName;
                qUser.BirthDate = Convert.ToDateTime(Input.BirthDate);

                #region ویرایش تصویر پروفایل
                {
                    if (Input.ProfileImage != null)
                    {
                        if (qUser.ProfileImgId != null)
                        {
                            // حذف تصویر قبلی
                            await _FtpWapper.RemoveFileAsync(new InpRemoveFile { FileId = qUser.ProfileImgId.Value.ToString() });
                            qUser.tblFiles = null;
                        }

                        // اپلود تصویر جدید
                        var _UploadFileResult = await _FtpWapper.UplaodProfileImgAsync(new InpUplaodProfileImg { FormFile = Input.ProfileImage, UserId = Input.UserId });
                        if (_UploadFileResult.IsSucceeded == false)
                            return new OperationResult().Failed(_UploadFileResult.Message);

                        qUser.ProfileImgId = Guid.Parse(_UploadFileResult.Message);
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

                    string EncryptedData = $"{Input.UserId}, {NewEmail}, {Token}".AesEncrypt(AuthConst.SecretKey);

                    string _Url = Input.UrlToChangeEmail.Replace("[Token]", WebUtility.UrlEncode(EncryptedData));

                    await _EmailSender.SendAsync(NewEmail, _Localizer["ChangeEmailSubject"], await _TemplateApplication.GetEmailChangeTemplateAsync(new InpGetEmailChangeTemplate { LangCode = CultureInfo.CurrentCulture.Name, Url = _Url }));

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
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> ChangeEmailAsync(InpChangeEmail Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                string DecryptedToken = Input.Token.AesDecrypt(AuthConst.SecretKey);

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
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> ReSendSmsCodeAsync(InpReSendSmsCode Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                var qUser = await _UserRepository.FindByPhoneNumberAsync(Input.PhoneNumber);
                if (qUser == null)
                    return new OperationResult().Failed("PhoneNumberNotFound");

                if (qUser.IsActive == false)
                    return new OperationResult().Failed("YourAccountIsDisabled");

                if (qUser.LastTrySentSms.HasValue)
                    if (qUser.LastTrySentSms.Value.AddMinutes(AuthConst.LimitToResendSmsInMinute) > DateTime.Now)
                        return new OperationResult().Failed("LimitToResendSms2Minute");

                var ReNewPasswordResult = await ReCreatePasswordAsync(new InpReCreatePassword { UserId = qUser.Id.ToString() });
                if (ReNewPasswordResult.IsSucceeded)
                {
                    var IsSend = _SmsSender.SendLoginCode(Input.PhoneNumber, ReNewPasswordResult.Message);
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
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> PhoneConfirmationBySmsCodeAsync(InpPhoneConfirmationBySmsCode Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                var qUser = await _UserRepository.FindByIdAsync(Input.UserId);
                if (qUser == null)
                    return new OperationResult().Failed("UserIdNotFound");

                if (qUser.PhoneNumber != Input.PhoneNumber)
                    return new OperationResult().Failed("PhoneNumberNotFound");

                if (qUser.PhoneNumberConfirmed)
                    return new OperationResult().Failed("PhoneNumberNotFound");

                if (qUser.PasswordPhoneNumber != Input.Code.ToMD5())
                    return new OperationResult().Failed("CodeIsInvalid");

                qUser.PhoneNumberConfirmed = true;

                await _UserRepository.UpdateAsync(qUser, default, true);

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> ChanagePasswordAsync(InpChanagePassword Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                var qUser = await _UserRepository.FindByIdAsync(Input.UserId);
                if (qUser == null)
                    return new OperationResult().Failed("UserNotFound");

                var Result = await _UserRepository.ChangePasswordAsync(qUser, Input.CurrentPassword, Input.NewPassword);
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
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed(ex.Message);
            }
        }

        public async Task<List<OutGetListForCombo>> GetListForComboAsync(InpGetListForCombo Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _UserRepository.Get
                                                 .Where(a => Input.LangId != null ? a.LangId == Guid.Parse(Input.LangId) : true)
                                                 .Select(a => new OutGetListForCombo
                                                 {
                                                     Id = a.Id.ToString(),
                                                     FullName = a.FirstName + " " + a.LastName,
                                                     ImgUrl = a.ProfileImgId != null ?
                                                                    a.tblProfileImage.tblFilePaths.tblFileServer.HttpDomin
                                                                    + a.tblProfileImage.tblFilePaths.tblFileServer.HttpPath
                                                                    + a.tblProfileImage.tblFilePaths.Path
                                                                    + a.tblProfileImage.FileName
                                                                : PublicConst.DefaultUserProfileImg
                                                 })
                                                 .Where(a => Input.Name != null ? a.FullName.Contains(Input.Name) : true)
                                                 .OrderBy(a => a.FullName)
                                                 .ToListAsync();

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<string> GetUserProfileImgUrlByIdAsync(InpGetUserProfileImgUrlById Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _UserRepository.Get
                                                 .Where(a => a.Id == Guid.Parse(Input.UserId))
                                                 .Select(a => new
                                                 {
                                                     ImgUrl = a.ProfileImgId != null ?
                                                                    a.tblProfileImage.tblFilePaths.tblFileServer.HttpDomin
                                                                    + a.tblProfileImage.tblFilePaths.tblFileServer.HttpPath
                                                                    + a.tblProfileImage.tblFilePaths.Path
                                                                    + a.tblProfileImage.FileName
                                                                : PublicConst.DefaultUserProfileImg
                                                 })
                                                 .SingleOrDefaultAsync();

                if (qData == null)
                    return PublicConst.DefaultUserProfileImg;

                return qData.ImgUrl;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return PublicConst.DefaultUserProfileImg;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return PublicConst.DefaultUserProfileImg;
            }
        }

        public async Task<OperationResult> RecoveryPasswordByEmailStep1Async(InpRecoveryPasswordByEmailStep1 Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                var qUser = await _UserRepository.FindByEmailAsync(Input.Email);
                if (qUser == null)
                    return new OperationResult().Failed("EmailNotFound");

                // ایجاد توکن بازیابی
                var _Token = await _UserRepository.GeneratePasswordResetTokenAsync(qUser);

                // رمز نگاری توکن
                string _EncryptedToken = $"{qUser.Id}, {_Token}".AesEncrypt(AuthConst.SecretKey);

                // ایجاد لینک بازیابی
                string _Url = Input.ResetLinkTemplate.Replace("[Token]", WebUtility.UrlEncode(_EncryptedToken));

                // ایجاد قالب ایمیل
                await _EmailSender.SendAsync(qUser.Email, _Localizer["RecoveryPassword"], await _TemplateApplication.GetEmailRecoveryPasswordTemplateAsync(new InpGetEmailRecoveryPasswordTemplate { LangCode = CultureInfo.CurrentCulture.Name, Url = _Url }));

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> RecoveryPasswordByEmailStep2Async(InpRecoveryPasswordByEmailStep2 Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion


                // رمزگشایی توکن
                string _DecryptToken = Input.Token.AesDecrypt(AuthConst.SecretKey);
                string _UserId = _DecryptToken.Split(", ")[0];
                string _Token = _DecryptToken.Split(", ")[1];

                // واکشی کاربر
                var qUser = await _UserRepository.FindByIdAsync(_UserId);
                if (qUser == null)
                    return new OperationResult().Failed("TokenIsInvalid");

                // بازنشانی
                var Result = await _UserRepository.ResetPasswordAsync(qUser, _Token, Input.NewPassword);
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
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }
    }
}
