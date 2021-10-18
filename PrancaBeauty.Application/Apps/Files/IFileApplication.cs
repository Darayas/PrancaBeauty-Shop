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
        Task<List<outGetFileDetailsForFileSelector>> GetFileDetailsForFileSelectorAsync(string[] FilesId);
        Task<OutGetFileInfo> GetFileInfoAsync(string FileId, string UserId = null);
        Task<(OutPagingData, List<OutGetFileListForFileManager>)> GetFileListForFileManagerAsync(InpGetFileListForFileManager Input);
        Task<string> GetFileUrlAsync(string FileId, string UserId = null);
        Task<OperationResult> RemoveFileAsync(string FileId, string UserId = null);
    }
}