using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ShowcaseTabSection
{
    public interface IShowcaseTabSectionApplication
    {
        Task<OperationResult> AddShowcaseTabSectionAsync(InpAddShowcaseTabSection Input);
        Task<OperationResult> EditShowcaseTabSectionAsync(InpEditShowcaseTabSection Input);
        Task<List<OutGetAllTabSectionForCombo>> GetAllTabSectionForComboAsync(InpGetAllTabSectionForCombo Input);
        Task<(OutPagingData, List<OutGetListShowcaseTabSectionForAdminPage>)> GetListShowcaseTabSectionForAdminPageAsync(InpGetListShowcaseTabSectionForAdminPage Input);
        Task<OutGetTabSectionForEdit> GetTabSectionForEditAsync(InpGetTabSectionForEdit Input);
        Task<OperationResult> RemoveShowcaseTabSectionAsync(InpRemoveShowcaseTabSection Input);
    }
}