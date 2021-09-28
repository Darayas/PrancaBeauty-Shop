using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Common.Utilities.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Files;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.CompoFileManager.CompoModalFileManager.Components
{
    public class Compo_ModalFileListModel : PageModel
    {
        private readonly IFileApplication _FileApplication;
        public Compo_ModalFileListModel(IFileApplication fileApplication)
        {
            _FileApplication = fileApplication;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_ModalFileList Input { get; set; }
        public List<vmCompo_ModalFileList> Data { get; set; }
        public OutPagingData PagingData { get; set; }
    }
}
