using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Application.Contracts.Users;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Users
{
    public interface IUserApplication
    {
        Task<OperationResult> AddUserAsync(InpAddUser Input);
        Task<OperationResult> EmailConfirmationAsync(string UserId, string Token);
        Task<string> GenerateEmailConfirmationTokenAsync(string UserId);
        Task<OperationResult> LoginByUserNamePasswordAsync(string UserName, string Pawword);
    }
}