using Framework.Common.Utilities.Paging;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Categories;
using PrancaBeauty.Application.Exceptions;
using PrancaBeauty.Domin.Categories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Categories
{
    public class CategoryApplication : ICategoryApplication
    {
        private readonly ILogger _Logger;
        private readonly ICategoryRepository _CategoryRepository;

        public CategoryApplication(ICategoryRepository categoryRepository, ILogger logger)
        {
            _CategoryRepository = categoryRepository;
            _Logger = logger;
        }

        public async Task<(OutPagingData, List<OutGetListForAdminPage>)> GetListForAdminPageAsync(string LangId, string Title,string ParentTitle, int PageNum, int Take)
        {
            try
            {
                if (PageNum < 1)
                    throw new ArgumentInvalidException("PageNum < 1");

                if (Take < 1)
                    throw new ArgumentInvalidException("Take < 1");

                Title = string.IsNullOrWhiteSpace(Title) ? null : Title;

                // آماده سازی اولیه ی کویری
                var qData = _CategoryRepository.Get.Select(a => new OutGetListForAdminPage
                {
                    Id = a.Id.ToString(),
                    ParentId = a.ParentId.ToString(),
                    Name = a.Name,
                    Title = a.tblCategory_Translates.Where(b => b.LangId == Guid.Parse(LangId)).Select(b => b.Title).Single(),
                    FontIconCode = a.FontIconCode,
                    Sort = a.Sort,
                    ParentTitle = a.tblCategory_Parent.tblCategory_Translates.Where(b => b.LangId == Guid.Parse(LangId)).Select(b => b.Title).Single(),
                })
                .Where(a => Title != null ? a.Title.Contains(Title) : true)
                .Where(a => ParentTitle != null ? a.ParentTitle.Contains(ParentTitle) : true)
                .OrderBy(a => a.ParentId)
                .ThenBy(a => a.Sort);

                // صفحه بندی داده ها
                var qPagingData = PagingData.Calc(await qData.LongCountAsync(), PageNum, Take);

                return (qPagingData, await qData.Skip((int)qPagingData.Skip).Take(qPagingData.Take).ToListAsync());
            }
            catch (ArgumentInvalidException)
            {
                return (null, null);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return (null, null);
            }
        }
    }
}
