using AutoMapper;
using Framework.Infrastructure;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Slider;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Categories;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Admin.Slider
{
    [Authorize(Roles = Roles.CanViewListSlider)]
    public class ListSliderModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IMapper _Mapper;
        private readonly ISliderApplication _SliderApplication;

        public ListSliderModel(IMsgBox msgBox, ILocalizer localizer, IMapper mapper, ISliderApplication sliderApplication)
        {
            _MsgBox=msgBox;
            _Localizer=localizer;
            _Mapper=mapper;
            _SliderApplication=sliderApplication;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request, string LangId)
        {
            var qData = await _SliderApplication.GetListForAdminPageAsync(new InpGetListForAdminPage
            {
                LangId = LangId,
                Title = Input.Title,
                ParentTitle = Input.ParentTitle,
                PageNum = request.Page,
                Take = request.PageSize
            });

            //var qListData = _Mapper.Map<List<vmCategoriesList>>(qData.Item2);

            //var _DataGrid = qListData.ToDataSourceResult(request);
            //_DataGrid.Total = (int)qData.Item1.CountAllItem;
            //_DataGrid.Data = qListData;

            //return new JsonResult(_DataGrid);

            return default;
        }

        public async Task<IActionResult> OnPostRemoveAsync(viListSliderRemove Input)
        {
            if (!User.IsInRole(Roles.CanRemoveSlide))
                return _MsgBox.InfoMsg(_Localizer["AccessDenied"]);

            //if (string.IsNullOrWhiteSpace(Id))
            //    return _MsgBox.ModelStateMsg("IdCantBeNull", "RefreshData()");

            //var _Result = await _CategoryApplication.RemoveCategoryAsync(new InpRemoveCategory { Id = Id });
            //if (_Result.IsSucceeded)
            //{
            //    return _MsgBox.SuccessMsg(_Localizer[_Result.Message], "RefreshData()");
            //}
            //else
            //{
            //    return _MsgBox.FaildMsg(_Localizer[_Result.Message]);
            //}

            return default;
        }

        [BindProperty(SupportsGet = true)]
        public viListSlider Input { get; set; }
    }
}
