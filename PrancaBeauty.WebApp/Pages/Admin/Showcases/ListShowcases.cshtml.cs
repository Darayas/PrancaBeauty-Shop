using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Showcases;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Sliders;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Types;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Admin.Showcases
{
    [Authorize(Roles = Roles.CanViewListShowcase)]
    public class ListShowcasesModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseApplication _ShowcaseApplication;

        public ListShowcasesModel(ILogger logger, IMapper mapper, IServiceProvider serviceProvider, IShowcaseApplication showcaseApplication, IMsgBox msgBox, ILocalizer localizer)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ServiceProvider=serviceProvider;
            _ShowcaseApplication=showcaseApplication;
            _MsgBox=msgBox;
            _Localizer=localizer;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request, string LangId)
        {
            var qData = await _ShowcaseApplication.GetListShowcaseForAdminPageAsync(new InpGetListShowcaseForAdminPage
            {
                LangId=LangId,
                CountryId=Input.CountryId,
                Title = Input.Title,
                Page = request.Page,
                Take = request.PageSize
            });


            var _DataGrid = qData.Item2.ToDataSourceResult(request);
            _DataGrid.Total = (int)qData.Item1.CountAllItem;
            _DataGrid.Data =_Mapper.Map<List<vmListShowcases>>(qData.Item2);

            return new JsonResult(_DataGrid);
        }

        public async Task<IActionResult> OnPostRemoveAsync(viListShowcaseRemove Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _Result = await _ShowcaseApplication.RemoveShowcaseAsync(new InpRemoveShowcase { Id=Input.Id });
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

        public async Task<IActionResult> OnPostSortingAsync(viListShowcaseSorting Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion


                var _Result = await _ShowcaseApplication.SortingShowcaseAsync(new InpSortingShowcase { Id=Input.Id, Act= (InpSortingShowcaseSortingItem)Input.Act });
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

        [BindProperty(SupportsGet = true)]
        public viListShowcases Input { get; set; }
    }
}
