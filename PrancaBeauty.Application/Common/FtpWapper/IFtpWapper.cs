using Microsoft.AspNetCore.Http;
using PrancaBeauty.Application.Contracts.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Common.FtpWapper
{
    public interface IFtpWapper
    {
        Task<bool> RemoveFileAsync(string FileId, string UserId = null);
        Task<OperationResult> UplaodCategoryImgAsync(IFormFile _FormFile,string _UserId);
        Task<OperationResult> UplaodProfileImgAsync(IFormFile _FormFile, string _UserId);
        Task<OperationResult> UploadFromFileManagerAsync(IFormFile _FormFile, string _UserId);
    }
}