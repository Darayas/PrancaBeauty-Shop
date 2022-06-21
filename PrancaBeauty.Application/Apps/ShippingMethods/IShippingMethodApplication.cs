using PrancaBeauty.Application.Contracts.ApplicationDTO.ShippingMethods;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ShippingMethods
{
    public interface IShippingMethodApplication
    {
        Task<List<OutGetShippingMethodForBill>> GetShippingMethodForBillAsync(InpGetShippingMethodForBill Input);
    }
}