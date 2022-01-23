using PrancaBeauty.Application.Contracts.Sellers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Seller
{
    public interface ISellerApplication
    {
        Task<List<OutGetListSellerForCombo>> GetListSellerForComboAsync(InpGetListSellerForCombo Input);
        Task<string> GetSellerIdByUserIdAsync(InpGetSellerIdByUserId Input);
    }
}