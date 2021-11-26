using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Infrastructure;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Categories;
using PrancaBeauty.Application.Contracts.Categories;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;

namespace PrancaBeauty.WebApp.Pages.Admin.Categories
{
    [Authorize(Roles = Roles.CanViewListCategories)]
    public class ListModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IMapper _Mapper;
        private readonly ICategoryApplication _CategoryApplication;

        public ListModel(ICategoryApplication CtegoryApplication, IMsgBox msgBox, ILocalizer localizer, IMapper mapper)
        {
            _CategoryApplication = CtegoryApplication;
            _MsgBox = msgBox;
            _Localizer = localizer;
            _Mapper = mapper;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request, string LangId)
        {
            var qData = await _CategoryApplication.GetListForAdminPageAsync(new InpGetListForAdminPage
            {
                LangId = LangId,
                Title = Input.Title,
                ParentTitle = Input.ParentTitle,
                PageNum = request.Page,
                Take = request.PageSize
            });

            var qListData = _Mapper.Map<List<vmCategoriesList>>(qData.Item2);

            var _DataGrid = qListData.ToDataSourceResult(request);
            _DataGrid.Total = (int)qData.Item1.CountAllItem;
            _DataGrid.Data = qListData;

            return new JsonResult(_DataGrid);
        }

        public async Task<IActionResult> OnPostRemoveAsync(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
                return _MsgBox.ModelStateMsg("IdCantBeNull", "RefreshData()");

            var _Result = await _CategoryApplication.RemoveCategoryAsync(new InpRemoveCategory { Id = Id });
            if (_Result.IsSucceeded)
            {
                return _MsgBox.SuccessMsg(_Localizer[_Result.Message], "RefreshData()");
            }
            else
            {
                return _MsgBox.FaildMsg(_Localizer[_Result.Message]);
            }
        }

        [BindProperty(SupportsGet = true)]
        public viListCategories Input { get; set; }
    }
}
