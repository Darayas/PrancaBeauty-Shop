using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Apps.ShowcaseTabs;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTab;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Admin.Showcases.Tabs
{
    [Authorize(Roles = Roles.CanEditShowcaseTab)]
    public class EditShowcaseTabModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseTabApplication _ShowcaseTabApplication;

        public EditShowcaseTabModel(ILogger logger, IMsgBox msgBox, IMapper mapper, ILocalizer localizer, IServiceProvider serviceProvider, IShowcaseTabApplication showcaseTabApplication)
        {
            _Logger=logger;
            _MsgBox=msgBox;
            _Mapper=mapper;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _ShowcaseTabApplication=showcaseTabApplication;
        }

        public async Task<IActionResult> OnGetAsync(viGetEditShowcaseTab Input, string ReturnUrl = null)
        {
            try
            {
                #region validatrions
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ShowcaseTabApplication.GetShowcaseTabForEditAsync(new InpGetShowcaseTabForEdit { Id=Input.Id }); ;
                if (qData==null)
                    return StatusCode(400);

                this.Input =_Mapper.Map<viEditShowcaseTab>(qData);

                ViewData["ReturnUrl"]=ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/Admin/ShowcaseTabs/List/{this.Input.ShowcaseId}";

                return Page();
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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                #region validatrions
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _MappedData = _Mapper.Map<InpSaveEditShowcaseTab>(Input);

                var _Result = await _ShowcaseTabApplication.SaveEditShowcaseTabAsync(_MappedData);
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
        public viEditShowcaseTab Input { get; set; }
    }
}
