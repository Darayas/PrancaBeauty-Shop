using PrancaBeauty.Application.Contracts.KeywordProducts;
using PrancaBeauty.Application.Contracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.KeywordsProducts
{
    public interface IKeywordProductsApplication
    {
        Task<OperationResult> AddKeywordsToProductAsync(InpAddKeywordsToProduct Input);
        Task<OperationResult> EditProductKeywordsAsync(InpEditProductKeywords Input);
        Task<List<OutGetKeywordByProductId>> GetKeywordByProductIdAsync(InpGetKeywordByProductId Input);
        Task<OperationResult> RemoveAllProductKeywordsAsync(InpRemoveAllProductKeywords Input);
    }
}