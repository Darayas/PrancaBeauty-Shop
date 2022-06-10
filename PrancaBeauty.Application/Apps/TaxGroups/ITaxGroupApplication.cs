using PrancaBeauty.Application.Contracts.ApplicationDTO.TaxGroup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.TaxGroups
{
    public interface ITaxGroupApplication
    {
        Task<List<OutGetListTaxGroupForCombo>> GetListTaxGroupForComboAsync(InpGetListTaxGroupForCombo Input);
    }
}