using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Users;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Users
{
    public interface IUserApplication
    {
        Task<OperationResult> AddUserAsync(InpAddUser Input);
        Task<OperationResult> ChanageUserAccessLevelAsync(InpChanageUserAccessLevel Input);
        Task<OperationResult> ChangeEmailAsync(InpChangeEmail Input);
        Task<OperationResult> ChangeUserStatusAsync(InpChangeUserStatus Input);
        Task<OperationResult> ChanagePasswordAsync(InpChanagePassword Input);
        Task<OperationResult> EditUsersRoleByAccIdAsync(InpEditUsersRoleByAccId Input);
        Task<OperationResult> EmailConfirmationAsync(InpEmailConfirmation Input);
        Task<string> GenerateEmailConfirmationTokenAsync(InpGenerateEmailConfirmationToken Input);
        Task<OutGetAllUserDetails> GetAllUserDetailsAsync(InpGetAllUserDetails Input);
        Task<(OutPagingData, List<OutGetListForAdminPage>)> GetListForAdminPageAsync(InpGetListForAdminPage Input);
        Task<tblUsers> GetUserByIdAsync(InpGetUserById Input);
        Task<tblUsers> GetUserByPhoneNumberAsync(InpGetUserByPhoneNumber Input);
        Task<OutGetUserDetailsForAccountSettings> GetUserDetailsForAccountSettingsAsync(InpGetUserDetailsForAccountSettings Input);
        Task<List<string>> GetUserIdsByAccIdAsync(InpGetUserIdsByAccId Input);
        Task<OperationResult> LoginByEmailLinkStep1Async(InpLoginByEmailLinkStep1 Input);
        Task<OperationResult> LoginByEmailLinkStep2Async(InpLoginByEmailLinkStep2 Input);
        Task<OperationResult> LoginByPhoneNumberStep1Async(InpLoginByPhoneNumberStep1 Input);
        Task<OperationResult> LoginByPhoneNumberStep2Async(InpLoginByPhoneNumberStep2 Input);
        Task<OperationResult> LoginByUserNamePasswordAsync(InpLoginByUserNamePassword Input);
        Task<OperationResult> PhoneConfirmationBySmsCodeAsync(InpPhoneConfirmationBySmsCode Input);
        Task<OperationResult> ReCreatePasswordAsync(InpReCreatePassword Input);
        Task<OperationResult> RemoveUserAsync(InpRemoveUser Input);
        Task<OperationResult> ReSendSmsCodeAsync(InpReSendSmsCode Input);
        Task<OperationResult> SaveAccountSettingUserDetailsAsync( InpSaveAccountSettingUserDetails Input);
        Task<List<OutGetListForCombo>> GetListForComboAsync(InpGetListForCombo Input);
        Task<string> GetUserProfileImgUrlByIdAsync(InpGetUserProfileImgUrlById Input);
        Task<OperationResult> LoginByEmailPasswordAsync(InpLoginByEmailPassword Input);
        Task<OperationResult> RecoveryPasswordByEmailStep1Async(InpRecoveryPasswordByEmailStep1 Input);
        Task<OperationResult> RecoveryPasswordByEmailStep2Async(InpRecoveryPasswordByEmailStep2 Input);
        Task<OperationResult> LoginAsync(InpLogin Input);
        Task<OperationResult> AddRolesAsync(InpAddRoles Input);
    }
}