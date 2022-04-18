using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Apps.ShowcaseTabs;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Admin.Showcases.Tabs
{
    [Authorize(Roles = Roles.CanAddShowcaseTab)]
    public class AddShowcaseTabModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseTabApplication _ShowcaseTabApplication;
        private readonly ILanguageApplication _LanguageApplication;

        public AddShowcaseTabModel(ILogger logger, IMsgBox msgBox, IMapper mapper, ILocalizer localizer, IServiceProvider serviceProvider, IShowcaseTabApplication showcaseTabApplication, ILanguageApplication languageApplication)
        {
            _Logger=logger;
            _MsgBox=msgBox;
            _Mapper=mapper;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _ShowcaseTabApplication=showcaseTabApplication;
            _LanguageApplication=languageApplication;
        }

        public async Task<IActionResult> OnGetAsync(viGetAddShowcaseTab Input, string ReturnUrl = null)
        {
            try
            {
                #region validatrions
                Input.CheckModelState(_ServiceProvider);
                #endregion

                ViewData["Id"]=Input.ShowcaseId;
                ViewData["ReturnUrl"]=ReturnUrl??$"/{CultureInfo.CurrentCulture.Parent.Name}/Admin/ShowcaseTab/List";

                var qLang = await _LanguageApplication.GetAllLanguageForSiteLangAsync();
                this.Input = new viAddShowcaseTab() { LstTranslate = qLang.Select(a => new viAddShowcaseTab_Translate { LangId=a.Id }).ToList() };

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
                #region validatrions
                Input.CheckModelState(_ServiceProvider);
                #endregion



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
        public viAddShowcaseTab Input { get; set; }
    }
}
