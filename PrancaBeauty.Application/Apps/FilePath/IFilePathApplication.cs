using PrancaBeauty.Application.Contracts.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.FilePath
{
    public interface IFilePathApplication
    {
        Task<bool> CheckDirectoryExistAsync(string FileServerId, string Path);
        Task<string> GetIdByPathAsync(string FileServerId, string Path);
        Task<OperationResult> MakePathAsync(string FileServerId, string Path);
    }
}