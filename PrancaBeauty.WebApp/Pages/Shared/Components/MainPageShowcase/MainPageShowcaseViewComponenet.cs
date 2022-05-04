using AutoMapper;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using PrancaBeauty.Application.Apps.Showcases;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.MainPageShowcase
{
    public class MainPageShowcaseViewComponenet : ViewComponent
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IShowcaseApplication _ShowcaseApplication;
        public MainPageShowcaseViewComponenet(ILogger logger, IMapper mapper, IShowcaseApplication ShowcaseApplication)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ShowcaseApplication=ShowcaseApplication;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                
                return View("MainPageShowcaseViewComponent");
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
