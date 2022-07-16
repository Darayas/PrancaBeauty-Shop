using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductGroupPercent;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductGroupPercent
{
    public interface IProductGroupPercentApplication
    {
        Task<OperationResult<List<OutGetProductGroupPercents>>> GetProductGroupPercentsAsync(InpGetProductGroupPercents Input);
    }
}