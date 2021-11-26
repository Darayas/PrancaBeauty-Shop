using PrancaBeauty.Application.Contracts.FilePath;
using PrancaBeauty.Application.Contracts.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.FilePath
{
    public interface IFilePathApplication
    {
        Task<bool> CheckDirectoryExistAsync(InpCheckDirectoryExist Input);
        Task<string> GetIdByPathAsync(InpGetIdByPath Input);
        Task<OperationResult> MakePathAsync(InpMakePath Input);
    }
}