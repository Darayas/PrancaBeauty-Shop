using PrancaBeauty.Application.Contracts.FileServer;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.FileServer
{
    public interface IFileServerApplication
    {
        Task<OutGetServerDetails> GetServerDetailsAsync(string ServerName);
    }
}