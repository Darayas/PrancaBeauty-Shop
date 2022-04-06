using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductVariants
{
    public interface IProductVariantsApplication
    {
        Task<List<outGetLstForCombo>> GetLstForComboAsync(InpGetLstForCombo Input);
    }
}