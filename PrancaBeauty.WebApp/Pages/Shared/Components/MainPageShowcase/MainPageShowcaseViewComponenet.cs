﻿using AutoMapper;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using PrancaBeauty.Application.Apps.Showcases;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System.Collections.Generic;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;

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

        public async Task<IViewComponentResult> InvokeAsync(viMainPageShowcase Input)
        {
            try
            {
                var qData = await _ShowcaseApplication.GetItemsForMainPageShowcaseAsync(new InpGetItemsForMainPageShowcase
                {
                    CountryId = Input.CountryId,
                    CurrencyId = Input.CurrencyId,
                    LangId=Input.LangId
                });

                if (qData== null)
                    return default;

                var _MappedData = _Mapper.Map<List<vmMainPageShowcase>>(qData);

                return View("MainPageShowcaseViewComponent", _MappedData);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
