using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.CompoFileManager.CompoModalFileManager.Components
{
    [Authorize]
    public class Compo_ModalFileListFilterModel : PageModel
    {
        public void OnGet()
        {
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_ModalFileListFilter Input { get; set; }
    }
}
