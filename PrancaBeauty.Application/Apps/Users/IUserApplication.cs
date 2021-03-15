using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Application.Contracts.Users;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Users
{
    public interface IUserApplication
    {
        Task<OperationResult> AddUserAsync(InpAddUser Input);
        Task<OperationResult> EmailConfirmationAsync(string UserId, string Token);
        Task<string> GenerateEmailConfirmationTokenAsync(string UserId);
        Task<OutGetAllUserDetails> GetAllUserDetailsAsync(string UserId);
        Task<tblUsers> GetUserByEmailAsync(string Email);
        Task<tblUsers> GetUserByIdAsync(string UserId);
        Task<tblUsers> GetUserByPhoneNumberAsync(string PhoneNumber);
        Task<OperationResult> LoginByEmailLinkStep1Async(string Email, string IP);
        Task<OperationResult> LoginByEmailLinkStep2Async(string UserId, string Password, string LinkIP, string UserIP, DateTime Date);
        Task<OperationResult> LoginByPhoneNumberStep1Async(string PhoneNumber);
        Task<OperationResult> LoginByPhoneNumberStep2Async(string PhoneNumber, string Code);
        Task<OperationResult> LoginByUserNamePasswordAsync(string UserName, string Pawword);
        Task<bool> RemoveUnConfirmedUserAsync(string UserId);
    }
}