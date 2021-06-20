using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Infrastructure;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp;
using PrancaBeauty.Application.Apps.Address;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewModel;

namespace PrancaBeauty.WebApp.Pages.User.EditProfile.Components.Address
{
    public class Compo_ListAddressModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IMapper _Mapper;
        private readonly IUserApplication _UserApplication;
        private readonly IAddressApplication _AddressApplication;
        private readonly ILanguageApplication _LanguageApplication;

        public Compo_ListAddressModel(IMsgBox msgBox, ILocalizer localizer, IUserApplication userApplication, IAddressApplication addressApplication, ILanguageApplication languageApplication, IMapper mapper)
        {
            _MsgBox = msgBox;
            _Localizer = localizer;
            _UserApplication = userApplication;
            _AddressApplication = addressApplication;
            _LanguageApplication = languageApplication;
            _Mapper = mapper;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostReadDataAsync(string LangId, [DataSourceRequest] DataSourceRequest request)
        {
            string UserId = User.GetUserDetails().UserId;
            //string LangId = await _LanguageApplication.GetLangIdByLangCodeAsync(CultureInfo.CurrentCulture.Name);

            var qData = await _AddressApplication.GetAddressByUserIdForManageAsync(UserId, LangId, null, request.Page, request.PageSize);

            var _DataGrid = qData.Item2.ToDataSourceResult(request);
            _DataGrid.Total = (int)qData.Item1.CountAllItem;
            _DataGrid.Data = _Mapper.Map<List<vmCompo_ListAddress>>(qData.Item2);

            return new JsonResult(_DataGrid);
        }

        public async Task<IActionResult> OnPostRemoveAsync(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
                return _MsgBox.ModelStateMsg("NotFound");

            string UserId = User.GetUserDetails().UserId;
            var Result = await _AddressApplication.RemoveAddressAsync(UserId, Id);
            if (Result.IsSucceeded)
            {
                return _MsgBox.SuccessMsg(_Localizer[Result.Message], "RefreshGrid('ListAddress');");
            }
            else
            {
                return _MsgBox.FaildMsg(_Localizer[Result.Message]);
            }
        }
    }
}
