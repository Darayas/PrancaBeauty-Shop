using PrancaBeauty.Application.Contracts.ProductPrice;
using PrancaBeauty.Application.Contracts.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductPrices
{
    public interface IProductPriceApplication
    {
        Task<OperationResult> AddPriceToProductAsyc(InpAddPriceToProduct Input);
        Task<OperationResult> RemovePriceFromProductAsync(InpRemovePriceFromProduct Input);
    }
}