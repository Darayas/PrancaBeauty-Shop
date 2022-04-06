using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductPropertiesValues;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductPropertiesValues
{
    public interface IProductPropertiesValuesApplication
    {
        Task<OperationResult> AddPropertiesToProductAsync(InpAddPropertiesToProduct Input);
        Task<OperationResult> EditProductPropertiesAsync(InpEditProductProperties Input);
        Task<OperationResult> RemovePropertiesByProductIdAsync(InpRemovePropertiesByProductId Input);
    }
}