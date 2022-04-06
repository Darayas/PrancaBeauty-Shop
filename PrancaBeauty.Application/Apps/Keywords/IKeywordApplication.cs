using PrancaBeauty.Application.Contracts.ApplicationDTO.Keywords;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Keywords
{
    public interface IKeywordApplication
    {
        Task<OperationResult> AddKeywordAsync(InpAddKeyword Input);
        Task<bool> CheckExistByTitleAsync(InpCheckExistByTitle Input);
        Task<string> GetIdByTitleAsync(InpGetIdByTitle Input);
    }
}