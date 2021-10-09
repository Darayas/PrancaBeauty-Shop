using PrancaBeauty.Application.Contracts.FileTypes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.FileTypes
{
    public interface IFileTypeApplication
    {
        Task<string[]> GetAlllFileExtentionAsync();
        Task<string> GetIdByMimeTypeAsync(string MimeType);
        Task<List<outGetListForCombo>> GetListForComboAsync(string Title);
    }
}