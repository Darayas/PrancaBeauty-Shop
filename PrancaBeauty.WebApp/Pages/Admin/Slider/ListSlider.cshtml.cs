using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Slider;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Categories;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Sliders;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Types;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Admin.Slider
{
    [Authorize(Roles = Roles.CanViewListSlider)]
    public class ListSliderModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ISliderApplication _SliderApplication;

        public ListSliderModel(IMsgBox msgBox, ILocalizer localizer, IMapper mapper, ISliderApplication sliderApplication, ILogger logger, IServiceProvider serviceProvider)
        {
            _MsgBox=msgBox;
            _Localizer=localizer;
            _Mapper=mapper;
            _SliderApplication=sliderApplication;
            _Logger=logger;
            _ServiceProvider=serviceProvider;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request, string LangId)
        {
            var qData = await _SliderApplication.GetListSlideForManageAsync(new InpGetListSlideForManage
            {
                Title = Input.Title,
                CurrentPage = request.Page,
                Take = request.PageSize
            });

            var qListData = _Mapper.Map<List<vmSliderList>>(qData.LstSlider);

            var _DataGrid = qListData.ToDataSourceResult(request);
            _DataGrid.Total = (int)qData.Paging.CountAllItem;
            _DataGrid.Data = qListData;

            return new JsonResult(_DataGrid);
        }

        public async Task<IActionResult> OnPostRemoveAsync(viListSliderRemove Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _Result = await _SliderApplication.RemoveSliderAsync(new InpRemoveSlider { Id=Input.Id });
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

        public async Task<IActionResult> OnPostSortingAsync(viListSliderSorting Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion


                var _Result = await _SliderApplication.SortingSliderAsync(new InpSortingSlider { Id=Input.Id, Action= (InpSortingSliderItem)Input.Act });
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
        public viListSlider Input { get; set; }
    }
}
