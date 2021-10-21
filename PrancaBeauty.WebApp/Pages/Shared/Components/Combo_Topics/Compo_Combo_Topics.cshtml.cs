using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductTopic;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Combo_Topics
{
    public class Compo_Combo_TopicsModel : PageModel
    {
        private readonly IProductTopicApplication _ProductTopicApplication;

        public Compo_Combo_TopicsModel(IProductTopicApplication productTopicApplication)
        {
            _ProductTopicApplication = productTopicApplication;
        }

        public IActionResult OnGet()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.TopicId";

            return Page();
        }

        public async Task<IActionResult> OnGetReadAsync(string LangId, string Text)
        {
            var qData = await _ProductTopicApplication.GetListForComboAsync(LangId, Text);
            var Data = _Mapper.Map<List<vmCompo_ComboFileTypes>>(qData);
            return new JsonResult(Data);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Combo_Topics Input { get; set; }
    }
}
