using PrancaBeauty.Application.Contracts.FileServer;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.FileServer
{
    public interface IFileServerApplication
    {
        Task<string> GetBestServerNameByFileSizeAsync(long FileSize);
        Task<OutGetServerDetails> GetServerDetailsAsync(string ServerName);
    }
}