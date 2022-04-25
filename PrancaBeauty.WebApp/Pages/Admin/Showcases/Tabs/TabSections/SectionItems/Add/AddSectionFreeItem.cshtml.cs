using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Apps.SectionItems;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Admin.Showcases.Tabs.TabSections.SectionItems.Add
{
    [Authorize(Roles = Roles.CanAddShowcaseTabSectionItem)]
    public class AddSectionFreeItemModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILanguageApplication _LanguageApplication;
        private readonly IShowcaseTabSectionItemApplication _ShowcaseTabSectionItemApplication;

        public AddSectionFreeItemModel(ILogger logger, IMsgBox msgBox, IMapper mapper, ILocalizer localizer, IServiceProvider serviceProvider, IShowcaseTabSectionItemApplication showcaseTabSectionItemApplication, ILanguageApplication languageApplication)
        {
            _Logger=logger;
            _MsgBox=msgBox;
            _Mapper=mapper;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _ShowcaseTabSectionItemApplication=showcaseTabSectionItemApplication;
            _LanguageApplication=languageApplication;
        }

        public async Task<IActionResult> OnGetAsync(viGetAddSectionFreeItem Input, string ReturnUrl = null)
        {
            try
            {
                #region validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                ViewData["ShowcaseId"]=Input.ShowcaseId;
                ViewData["ShowcaseTabId"]=Input.ShowcaseTabId;
                ViewData["ShowcaseTabSectionId"]=Input.ShowcaseTabSectionId;
                ViewData["ReturnUrl"]=ReturnUrl??$"/{CultureInfo.CurrentCulture.Parent.Name}/Admin/ShowcaseTabSectionItems/List/{Input.ShowcaseId}/{Input.ShowcaseTabId}/{Input.ShowcaseTabSectionId}";

                var qLang = await _LanguageApplication.GetAllLanguageForSiteLangAsync();
                this.Input = new viAddSectionFreeItem()
                {
                    ShowcaseTabSectionId = Input.ShowcaseTabSectionId,
                    LstTranslate = qLang.Select(a => new viAddSectionFreeItemTranslate { LangId=a.Id }).ToList()
                };

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
                #region validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _MappedData = _Mapper.Map<InpAddTabSectionFreeItem>(Input);

                var _Result = await _ShowcaseTabSectionItemApplication.AddTabSectionFreeItemAsync(_MappedData);
                if (_Result.IsSucceeded)
                    return _MsgBox.SuccessMsg(_Localizer[_Result.Message], "GotoList()");
                else
                    return _MsgBox.FaildMsg(_Localizer[_Result.Message]);
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FaildMsg(_Localizer["Error500"]);
            }
        }

        [BindProperty]
        public viAddSectionFreeItem Input { get; set; }
    }
}
