using PrancaBeauty.Application.Contracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductPropertiesValues
{
    public interface IProductPropertiesValuesApplication
    {
        Task<OperationResult> AddPropertiesToProductAsync(string ProductId, Dictionary<string, string> PropItems);
    }
}