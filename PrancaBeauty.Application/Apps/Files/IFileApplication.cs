using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.Files;
using PrancaBeauty.Application.Contracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Files
{
    public interface IFileApplication
    {
        Task<OperationResult> AddFileAsync(InpAddFile Input);
        Task<List<outGetFileDetailsForFileSelector>> GetFileDetailsForFileSelectorAsync(List<InpGetFileDetailsForFileSelector> Input);
        Task<OutGetFileInfo> GetFileInfoAsync(InpGetFileInfo Input);
        Task<(OutPagingData, List<OutGetFileListForFileManager>)> GetFileListForFileManagerAsync(InpGetFileListForFileManager Input);
        Task<string> GetFileUrlAsync(InpGetFileUrl Input);
        Task<OperationResult> RemoveFileAsync(InpRemoveFile Input);
    }
}