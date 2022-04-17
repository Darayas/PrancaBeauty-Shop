using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ShowcaseTabs
{
    public interface IShowcaseTabApplication
    {
        Task<(OutPagingData PagingData, List<OutGetListShowcaseTabForAdminPage> LstData)> GetListShowcaseTabForAdminPageAsync(InpGetListShowcaseTabForAdminPage Input);
    }
}