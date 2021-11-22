using PrancaBeauty.Application.Contracts.Keywords;
using PrancaBeauty.Application.Contracts.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Keywords
{
    public interface IKeywordApplication
    {
        Task<OperationResult> AddKeywordAsync(InpAddKeyword Input);
        Task<bool> CheckExistByTitleAsync(string Title);
        Task<string> GetIdByTitleAsync(string Title);
    }
}