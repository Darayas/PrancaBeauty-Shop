using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Framework.Application.Services.Security.AntiShell
{
    public interface IAniShell
    {
        Task<(string, string)> GetRealExtentionAsync(IFormFile _FormFile);
        Task<bool> ValidationExtentionAsync(IFormFile _FormFile);
    }
}