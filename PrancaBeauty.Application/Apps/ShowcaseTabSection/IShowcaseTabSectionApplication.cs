using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ShowcaseTabSection
{
    public interface IShowcaseTabSectionApplication
    {
        Task<(OutPagingData, List<OutGetListShowcaseTabSectionForAdminPage>)> GetListShowcaseTabSectionForAdminPageAsync(InpGetListShowcaseTabSectionForAdminPage Input);
    }
}