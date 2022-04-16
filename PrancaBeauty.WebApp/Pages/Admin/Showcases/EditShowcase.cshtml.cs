using AutoMapper;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Showcases;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Admin.Showcases
{
    [Authorize(Roles =Roles.CanEditShowcase)]
    public class EditShowcaseModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseApplication _ShowcaseApplication;

        public EditShowcaseModel(ILogger logger, IMsgBox msgBox, IMapper mapper, ILocalizer localizer, IServiceProvider serviceProvider, IShowcaseApplication showcaseApplication)
        {
            _Logger=logger;
            _MsgBox=msgBox;
            _Mapper=mapper;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _ShowcaseApplication=showcaseApplication;
        }

        public async Task<IActionResult> OnGetAsync(viGetEditShowcase Input)
        {

            return Page();
        }

        [BindProperty]
        public viEditShowcase Input { get; set; }
    }
}
