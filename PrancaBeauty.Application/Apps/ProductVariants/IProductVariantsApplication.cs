using PrancaBeauty.Application.Contracts.ProductVariants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductVariants
{
    public interface IProductVariantsApplication
    {
        Task<List<outGetLstForCombo>> GetLstForComboAsync(string LangId, string Text);
    }
}