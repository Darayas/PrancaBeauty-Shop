using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Showcases
{
    public interface IShowcaseApplication
    {
        Task<OperationResult> AddShowcaseAsync(InpAddShowcase Input);
        Task<(OutPagingData, List<OutGetListShowcaseForAdminPage>)> GetListShowcaseForAdminPageAsync(InpGetListShowcaseForAdminPage Input);
    }
}