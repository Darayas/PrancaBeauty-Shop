using PrancaBeauty.Application.Contracts.ApplicationDTO.PostalBarcode;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.PostalBarcode
{
    public interface IPostalBarcodeApplication
    {
        Task<OperationResult> ChangeShipingMethodAsync(InpChangeShipingMethod Input);
    }
}