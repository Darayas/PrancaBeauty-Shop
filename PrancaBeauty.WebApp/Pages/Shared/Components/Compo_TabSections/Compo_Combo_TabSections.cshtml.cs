using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ShowcaseTabSection;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSections;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_TabSections
{
    public class Compo_Combo_TabSectionsModel : PageModel
    {
        private readonly IShowcaseTabSectionApplication _ShowcaseTabSectionApplication;
        public Compo_Combo_TabSectionsModel(IShowcaseTabSectionApplication showcaseTabSectionApplication)
        {
            _ShowcaseTabSectionApplication=showcaseTabSectionApplication;
        }

        public IActionResult OnGet()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.ShowcaseTabSectionId";

            return Page();
        }

        public async Task<JsonResult> OnGetReadAsync()
        {
            var qData = await _ShowcaseTabSectionApplication.GetAllTabSectionForComboAsync(new InpGetAllTabSectionForCombo
            {
                ShowcaseTabId=Input.ShowcaseTabId,
                Name=Input.Name
            });

            return new JsonResult(qData);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Combo_TabSections Input { get; set; }
    }
}
