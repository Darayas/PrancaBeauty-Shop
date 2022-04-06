using Microsoft.AspNetCore.Http;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Common.FtpWapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Common.FtpWapper
{
    public interface IFtpWapper
    {
        Task<bool> RemoveFileAsync(InpRemoveFile Input);
        Task<OperationResult> UplaodCategoryImgAsync(InpUplaodCategoryImg Input);
        Task<OperationResult> UplaodProfileImgAsync(InpUplaodProfileImg Input);
        Task<OperationResult> UploadFromFileManagerAsync(InpUploadFromFileManager Input);
    }
}