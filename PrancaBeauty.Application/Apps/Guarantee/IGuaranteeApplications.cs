using PrancaBeauty.Application.Contracts.ApplicationDTO.Guarantee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Guarantee
{
    public interface IGuaranteeApplications
    {
        Task<List<OutGetGuaranteeListForCombo>> GetListForComboAsync(InpGetListForCombo Input);
    }
}