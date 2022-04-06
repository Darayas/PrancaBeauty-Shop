using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductPrice;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductPrices
{
    public interface IProductPriceApplication
    {
        Task<OperationResult> AddPriceToProductAsyc(InpAddPriceToProduct Input);
        Task<OperationResult> RemovePriceFromProductAsync(InpRemovePriceFromProduct Input);
    }
}