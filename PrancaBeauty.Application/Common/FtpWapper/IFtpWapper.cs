using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Common.FtpWapper
{
    public interface IFtpWapper
    {
        Task<bool> RemoveFileAsync(string FileId, string UserId = null);
        Task<string> UplaodCategoryImgAsync(IFormFile _FormFile, string _FileName = null);
        Task<string> UplaodProfileImgAsync(IFormFile _FormFile, string _FileName = null);
    }
}