using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.CompoFileManager.CompoModalFileManager.Components
{
    public class Compo_ModalFileUploaderContainerModel : PageModel
    {
        public Compo_ModalFileUploaderContainerModel()
        {

        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_ModalFileUploaderContainer Input { get; set; }
    }
}
