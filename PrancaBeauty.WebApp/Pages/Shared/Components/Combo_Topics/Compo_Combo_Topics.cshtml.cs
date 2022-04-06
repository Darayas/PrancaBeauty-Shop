using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductTopic;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductTopics;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Combo_Topics
{
    public class Compo_Combo_TopicsModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly IProductTopicApplication _ProductTopicApplication;

        public Compo_Combo_TopicsModel(IProductTopicApplication productTopicApplication, IMapper mapper)
        {
            _ProductTopicApplication = productTopicApplication;
            _Mapper = mapper;
        }

        public IActionResult OnGet()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.TopicId";

            return Page();
        }

        public async Task<IActionResult> OnGetReadAsync(string LangId, string Text)
        {
            var qData = await _ProductTopicApplication.GetListForComboAsync(new InpGetListForCombo { LangId = LangId, Text = Text });
            var Data = _Mapper.Map<List<vmCompo_Combo_Topics>>(qData);
            return new JsonResult(Data);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Combo_Topics Input { get; set; }
    }
}
