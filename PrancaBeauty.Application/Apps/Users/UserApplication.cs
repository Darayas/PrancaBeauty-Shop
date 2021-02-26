using Framework.Infrastructure;
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

        public async Task<OperationResult> LoginByUserNamePasswordAsync(string UserName, string Pawword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserName))
                    throw new ArgumentNullException("UserName cant be null.");

                if (string.IsNullOrWhiteSpace(Pawword))
                    throw new ArgumentNullException("Pawword cant be null.");

                return new OperationResult().Succeeded("");
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
    }
}
