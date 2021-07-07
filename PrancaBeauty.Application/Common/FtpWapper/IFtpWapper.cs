using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Common.FtpWapper
{
    public interface IFtpWapper
    {
        Task<string> UplaodCategoryImgAsync(IFormFile _FormFile, string _FileName = null);
    }
}