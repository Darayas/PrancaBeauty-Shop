using PrancaBeauty.Application.Contracts.ApplicationDTO.FileTypes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.FileTypes
{
    public interface IFileTypeApplication
    {
        Task<string[]> GetAllFileExtentionAsync();
        Task<string[]> GetAllFileMimeTypeAsync();
        Task<string> GetIdByMimeTypeAsync(InpGetIdByMimeType Input);
        Task<List<outGetFileTypeListForCombo>> GetListForComboAsync(InpGetListForCombo Input);
    }
}