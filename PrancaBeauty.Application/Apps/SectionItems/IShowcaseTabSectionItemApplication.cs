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
        Task<OperationResult> AddTabSectionCategoryItemAsync(InpAddTabSectionCategoryItem Input);
        Task<OperationResult> AddTabSectionFreeItemAsync(InpAddTabSectionFreeItem Input);
        Task<OperationResult> AddTabSectionKeywordItemAsync(InpAddTabSectionKeywordItem Input);
        Task<OperationResult> AddTabSectionProductItemAsync(InpAddTabSectionProductItem Input);
        Task<OutGetCategoryItemForEdit> GetCategoryItemForEditAsync(InpGetCategoryItemForEdit Input);
        Task<OutGetFreeItemForEdit> GetFreeItemForEditAsync(InpGetFreeItemForEdit Input);
        Task<OutGetKeywordItemForEdit> GetKeywordItemForEditAsync(InpGetKeywordItemForEdit Input);
        Task<(OutPagingData, List<OutGetListShowcaseTabSectionItemForAdminPage>)> GetListShowcaseTabSectionItemForAdminPageAsync(InpGetListShowcaseTabSectionItemForAdminPage Input);
        Task<OutGetProductItemForEdit> GetProductItemForEditAsync(InpGetProductItemForEdit Input);
        Task<OperationResult> RemoveSectionItemAsync(InpRemoveSectionItem Input);
        Task<OperationResult> SaveEditCategoryItemAsync(InpSaveEditCategoryItem Input);
        Task<OperationResult> SaveEditFreeItemAsync(InpSaveEditFreeItem Input);
        Task<OperationResult> SaveEditKeywordItemAsync(InpSaveEditKeywordItem Input);
        Task<OperationResult> SaveEditProductItemAsync(InpSaveEditProductItem Input);
        Task<OperationResult> SortingSectionItemAsync(InpSortingSectionItem Input);
    }
}
