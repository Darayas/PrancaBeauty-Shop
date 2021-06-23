using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Categories
{
    public interface ICategoryApplication
    {
        Task<(OutPagingData, List<OutGetListForAdminPage>)> GetListForAdminPageAsync(string LangId, string Title, int PageNum, int Take);
    }
}