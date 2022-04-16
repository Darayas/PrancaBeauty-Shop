using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Apps.Showcases;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Admin.Showcases
{
    [Authorize(Roles = Roles.CanAddShowcase)]
    public class AddShowcaseModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseApplication _ShowcaseApplication;
        private readonly ILanguageApplication _LanguageApplication;

        public AddShowcaseModel(ILogger logger, IMsgBox msgBox, IMapper mapper, ILocalizer localizer, IServiceProvider serviceProvider, IShowcaseApplication showcaseApplication, ILanguageApplication languageApplication)
        {
            _Logger=logger;
            _MsgBox=msgBox;
            _Mapper=mapper;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _ShowcaseApplication=showcaseApplication;
            _LanguageApplication=languageApplication;
        }

        public async Task<IActionResult> OnGetAsync(string ReturnUrl = null)
        {
            ViewData["ReturnUrl"]=ReturnUrl??$"/{CultureInfo.CurrentCulture.Parent.Name}/Admin/Showcase/List";

            var qLang = await _LanguageApplication.GetAllLanguageForSiteLangAsync();
            Input = new viAddShowcase() { LstTranslate = qLang.Select(a => new viAddShowcase_Translate { LangId=a.Id }).ToList() };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _MappedData = _Mapper.Map<InpAddShowcase>(Input);
                _MappedData.UserId=User.GetUserDetails().UserId;

                var _Result = await _ShowcaseApplication.AddShowcaseAsync(_MappedData);
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
        public viAddShowcase Input { get; set; }
    }
}
