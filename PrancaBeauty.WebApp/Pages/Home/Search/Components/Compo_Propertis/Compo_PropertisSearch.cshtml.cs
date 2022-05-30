using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductPropertis;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductProperties;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Home.Search.Components.Compo_Propertis
{
    public class Compo_PropertisSearchModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductPropertisApplication _ProductPropertisApplication;

        public Compo_PropertisSearchModel(ILogger logger, IMapper mapper, IServiceProvider serviceProvider, IProductPropertisApplication productPropertisApplication)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ServiceProvider=serviceProvider;
            _ProductPropertisApplication=productPropertisApplication;
        }

        public async Task<IActionResult> OnGetAsync(string LangId)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductPropertisApplication.GetPropertiesForSearchAsync(new InpGetPropertiesForSearch
                {
                    LangId = LangId,
                    TopicId=Input.TopicId
                });
                if (qData==null)
                    return StatusCode(400);

                Data= _Mapper.Map<List<vmCompo_PropertisSearch>>(qData);

                return Page();
            }
            catch (ArgumentInvalidException)
            {
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return StatusCode(500);
            }
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_PropertisSearch Input { get; set; }
        public List<vmCompo_PropertisSearch> Data { get; set; }
    }
}
