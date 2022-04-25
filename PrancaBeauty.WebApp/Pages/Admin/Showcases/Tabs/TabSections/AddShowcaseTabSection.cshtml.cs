using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ShowcaseTabSection;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSections;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Admin.Showcases.Tabs.TabSections
{
    [Authorize(Roles = Roles.CanAddShowcaseTabSection)]
    public class AddShowcaseTabSectionModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseTabSectionApplication _ShowcaseTabSectionApplication;

        public AddShowcaseTabSectionModel(ILogger logger, IMsgBox msgBox, IMapper mapper, ILocalizer localizer, IServiceProvider serviceProvider, IShowcaseTabSectionApplication showcaseTabSectionApplication)
        {
            _Logger=logger;
            _MsgBox=msgBox;
            _Mapper=mapper;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _ShowcaseTabSectionApplication=showcaseTabSectionApplication;
        }

        public IActionResult OnGet(viGetAddShowcaseTabSection Input, string ReturnUrl = null)
        {
            try
            {
                #region validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                ViewData["ShowcaseId"]=Input.ShowcaseId;
                ViewData["ShowcaseTabId"]=Input.ShowcaseTabId;
                ViewData["ReturnUrl"]=ReturnUrl??$"/{CultureInfo.CurrentCulture.Parent.Name}/Admin/ShowcaseTabSection/List/"+Input.ShowcaseTabId;

                return Page();
            }
            catch (ArgumentInvalidException)
            {
                return StatusCode(400);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _Result = await _ShowcaseTabSectionApplication.AddShowcaseTabSectionAsync(_Mapper.Map<InpAddShowcaseTabSection>(Input));
                if (_Result.IsSucceeded)
                    return _MsgBox.SuccessMsg(_Localizer[_Result.Message], "GotoList()");
                else
                    return _MsgBox.FaildMsg(_Localizer[_Result.Message]);
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.FaildMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FaildMsg(_Localizer["Error500"]);
            }
        }

        [BindProperty]
        public viAddShowcaseTabSection Input { get; set; }
    }
}
