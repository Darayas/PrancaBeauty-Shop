using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductGroups;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductGroup
{
    public interface IProductGroupApplication
    {
        Task<List<OutGetListProductGroupForCombo>> GetListProductGroupForComboAsync(InpGetListProductGroupForCombo Input);
    }
}