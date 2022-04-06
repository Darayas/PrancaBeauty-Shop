using PrancaBeauty.Application.Contracts.ApplicationDTO.FileServer;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.FileServer
{
    public interface IFileServerApplication
    {
        Task<string> GetBestServerNameByFileSizeAsync(InpGetBestServerNameByFileSize  Input);
        Task<OutGetServerDetails> GetServerDetailsAsync(InpGetServerDetails Input);
    }
}