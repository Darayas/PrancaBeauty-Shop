using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.SectionItems
{
    public interface IShowcaseTabSectionItemApplication
    {
        Task<OperationResult> AddTabSectionFreeItemAsync(InpAddTabSectionFreeItem Input);
        Task<(OutPagingData, List<OutGetListShowcaseTabSectionItemForAdminPage>)> GetListShowcaseTabSectionItemForAdminPageAsync(InpGetListShowcaseTabSectionItemForAdminPage Input);
    }
}
