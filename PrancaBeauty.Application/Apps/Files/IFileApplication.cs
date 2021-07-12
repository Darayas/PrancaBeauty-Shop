using PrancaBeauty.Application.Contracts.Files;
using PrancaBeauty.Application.Contracts.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Files
{
    public interface IFileApplication
    {
        Task<OperationResult> AddFileAsync(InpAddFile Input);
        Task<OutGetFileInfo> GetFileInfoAsync(string FileId, string UserId = null);
        Task<OperationResult> RemoveFileAsync(string FileId, string UserId = null);
    }
}