using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ShowcaseTabs;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTab;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Types;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Admin.Showcases.Tabs
{
    [Authorize(Roles = Roles.CanViewListShowcaseTab)]
    public class ListShowcaseTabsModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILocalizer _Localizer;
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly IShowcaseTabApplication _ShowcaseTabApplication;

        public ListShowcaseTabsModel(ILogger logger, IServiceProvider serviceProvider, IMsgBox msgBox, IMapper mapper, IShowcaseTabApplication showcaseTabApplication, ILocalizer localizer)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _MsgBox=msgBox;
            _Mapper=mapper;
            _ShowcaseTabApplication=showcaseTabApplication;
            _Localizer=localizer;
        }

        public IActionResult OnGet(viGetListShowcaseTabs Input, string ReturnUrl = null)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                ViewData["Id"]=Input.ShowcaseId;
                ViewData["ReturnUrl"]=ReturnUrl??$"/{CultureInfo.CurrentCulture.Parent.Name}/Admin/Showcase/List";

                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request, string LangId)
        {
            var qData = await _ShowcaseTabApplication.GetListShowcaseTabForAdminPageAsync(new InpGetListShowcaseTabForAdminPage
            {
                ShowcaseId=Input.ShowcaseId,
                LangId=LangId,
                Title = Input.Title,
                Page = request.Page,
                Take = request.PageSize
            });

            var _DataGrid = qData.Item2.ToDataSourceResult(request);
            _DataGrid.Total = (int)qData.PagingData.CountAllItem;
            _DataGrid.Data =_Mapper.Map<List<vmListShowcaseTabs>>(qData.LstData);

            return new JsonResult(_DataGrid);
        }

        public async Task<IActionResult> OnPostSortingAsync(viListShowcaseTabSorting Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _Result = await _ShowcaseTabApplication.SortingShowcaseTabAsync(new InpSortingShowcaseTab { Id=Input.Id, Act= (InpSortingShowcaseTabItem)Input.Act, ShowcaseId=Input.ShowcaseId });
                if (_Result.IsSucceeded)
                {
                    return new JsResult("RefreshData()");
                }
                else
                {
                    return _MsgBox.FaildMsg(_Localizer[_Result.Message]);
                }
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> OnPostRemoveAsync(viListShowcaseTabRemove Input)
        {
            try
            {
                if (!User.IsInRole(Roles.CanRemoveShowcaseTab))
                    return _MsgBox.AccessDeniedMsg();

                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _Result = await _ShowcaseTabApplication.RemoveShowcaseTabAsync(new InpRemoveShowcaseTab { Id=Input.Id });
                if (_Result.IsSucceeded)
                {
                    return _MsgBox.SuccessMsg(_Localizer[_Result.Message], "RefreshData()");
                }
                else
                {
                    return _MsgBox.FaildMsg(_Localizer[_Result.Message]);
                }
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return StatusCode(500);
            }
        }

        [BindProperty]
        public viListShowcaseTabs Input { get; set; }
    }
}
