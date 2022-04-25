using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.SectionItems;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSections;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Admin.Showcases.Tabs.TabSections.SectionItems
{
    [Authorize(Roles = Roles.CanViewListShowcaseTabSectionItems)]
    public class ListSectionItemsModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILocalizer _Localizer;
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly IShowcaseTabSectionItemApplication _ShowcaseTabSectionItemApplication;

        public ListSectionItemsModel(ILogger logger, IServiceProvider serviceProvider, ILocalizer localizer, IMsgBox msgBox, IMapper mapper, IShowcaseTabSectionItemApplication showcaseTabSectionItemApplication)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _Localizer=localizer;
            _MsgBox=msgBox;
            _Mapper=mapper;
            _ShowcaseTabSectionItemApplication=showcaseTabSectionItemApplication;
        }

        public IActionResult OnGet(viGetListSectionItems Input, string ReturnUrl = null)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                ViewData["ShowcaseId"]=Input.ShowcaseId;
                ViewData["ShowcaseTabId"]=Input.ShowcaseTabId;
                ViewData["ShowcaseTabSectionId"]=Input.ShowcaseTabSectionId;
                ViewData["ReturnUrl"] = ReturnUrl??$"/{CultureInfo.CurrentCulture.Parent.Name}/Admin/ShowcaseTabSection/List/{Input.ShowcaseId}/{Input.ShowcaseTabId}";

                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request,string LangId)
        {
            var qData = await _ShowcaseTabSectionItemApplication.GetListShowcaseTabSectionItemForAdminPageAsync(new InpGetListShowcaseTabSectionItemForAdminPage
            {
                LangId=LangId,
                ShowcaseTabSectionId=Input.ShowcaseTabSectionId,
                Title = Input.Title,
                Page = request.Page,
                Take = request.PageSize
            });

            var _DataGrid = qData.Item2.ToDataSourceResult(request);
            _DataGrid.Total = (int)qData.Item1.CountAllItem;
            _DataGrid.Data =_Mapper.Map<List<vmListShowcaseTabSectionItem>>(qData.Item2);

            return new JsonResult(_DataGrid);
        }

        [BindProperty]
        public viListSectionItems Input { get; set; }
    }
}
