using PrancaBeauty.Application.Contracts.KeywordProducts;
using PrancaBeauty.Application.Contracts.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.KeywordsProducts
{
    public interface IKeywordProductsApplication
    {
        Task<OperationResult> AddKeywordsToProductAsync(InpAddKeywordsToProduct Input);
        Task<OperationResult> RemoveAllProductKeywordsAsync(InpRemoveAllProductKeywords Input);
    }
}