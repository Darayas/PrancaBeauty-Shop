using PrancaBeauty.Application.Contracts.ProductPropertiesValues;
using PrancaBeauty.Application.Contracts.Results;
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