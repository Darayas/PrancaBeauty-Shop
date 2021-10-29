using PrancaBeauty.Application.Contracts.Guarantee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Guarantee
{
    public interface IGuaranteeApplications
    {
        Task<List<OutGetListForCombo>> GetListForComboAsync(string LangId, string Search);
    }
}