using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ShowcaseTabs
{
    public interface IShowcaseTabApplication
    {
        Task<OperationResult> AddShowcaseTabAsync(InpAddShowcaseTab Input);
        Task<(OutPagingData PagingData, List<OutGetListShowcaseTabForAdminPage> LstData)> GetListShowcaseTabForAdminPageAsync(InpGetListShowcaseTabForAdminPage Input);
    }
}