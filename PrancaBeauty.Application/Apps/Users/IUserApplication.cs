using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Application.Contracts.Users;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Users
{
    public interface IUserApplication
    {
        Task<OperationResult> AddRolesAsync(tblUsers user, string[] Roles);
        Task<OperationResult> AddUserAsync(InpAddUser Input);
        Task<OperationResult> ChanageUserAccessLevelAsync(string UserId, string SelfUserId, string AccessLevelId);
        Task<OperationResult> ChangeEmailAsync(string Token);
        Task<OperationResult> ChangeUserStatusAsync(string UserId, string SelfUserId);
        Task<OperationResult> EditUsersRoleByAccIdAsync(string AccessLevelId, string[] Roles);
        Task<OperationResult> EmailConfirmationAsync(string UserId, string Token);
        Task<string> GenerateEmailConfirmationTokenAsync(string UserId);
        Task<OutGetAllUserDetails> GetAllUserDetailsAsync(string UserId);
        Task<(OutPagingData, List<OutGetListForAdminPage>)> GetListForAdminPageAsync(string Email, string PhoneNumber, string FullName, string Sort, int PageNum, int Take);
        Task<tblUsers> GetUserByEmailAsync(string Email);
        Task<tblUsers> GetUserByIdAsync(string UserId);
        Task<tblUsers> GetUserByPhoneNumberAsync(string PhoneNumber);
        Task<OutGetUserDetailsForAccountSettings> GetUserDetailsForAccountSettingsAsync(string UserId);
        Task<List<string>> GetUserIdsByAccIdAsync(string AccessLevelId);
        Task<OperationResult> LoginByEmailLinkStep1Async(string Email, string IP);
        Task<OperationResult> LoginByEmailLinkStep2Async(string UserId, string Password, string LinkIP, string UserIP, DateTime Date);
        Task<OperationResult> LoginByPhoneNumberStep1Async(string PhoneNumber);
        Task<OperationResult> LoginByPhoneNumberStep2Async(string PhoneNumber, string Code);
        Task<OperationResult> LoginByUserNamePasswordAsync(string UserName, string Pawword);
        Task<OperationResult> ReCreatePasswordAsync(tblUsers User);
        Task<OperationResult> RemoveAllRolesAsync(tblUsers user);
        Task<bool> RemoveUnConfirmedUserAsync(string UserId);
        Task<OperationResult> RemoveUserAsync(string UserId);
        Task<OperationResult> SaveAccountSettingUserDetailsAsync(string UserId, InpSaveAccountSettingUserDetails Input, string UrlToChangeEmail);
    }
}