using AutoMapper;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ShowcaseTabs;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;

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

        public AddShowcaseTabModel(ILogger logger, IMsgBox msgBox, IMapper mapper, ILocalizer localizer, IServiceProvider serviceProvider, IShowcaseTabApplication showcaseTabApplication)
        {
            _Logger=logger;
            _MsgBox=msgBox;
            _Mapper=mapper;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _ShowcaseTabApplication=showcaseTabApplication;
        }

        public IActionResult OnGet(string ReturnUrl = null)
        {
            return Page();
        }
    }
}
