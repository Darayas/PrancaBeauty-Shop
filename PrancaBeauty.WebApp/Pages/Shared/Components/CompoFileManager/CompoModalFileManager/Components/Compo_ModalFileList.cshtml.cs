using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Common.Utilities.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Files;
using PrancaBeauty.Application.Contracts.Files;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.CompoFileManager.CompoModalFileManager.Components
{
    [Authorize]
    public class Compo_ModalFileListModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly IFileApplication _FileApplication;
        public Compo_ModalFileListModel(IFileApplication fileApplication, IMapper mapper)
        {
            _FileApplication = fileApplication;
            _Mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.IsInRole(Roles.CanManageAllUserFliles))
                Input.UserId = User.GetUserDetails().UserId;



            var qData = await _FileApplication.GetFileListForFileManagerAsync(_Mapper.Map<InpGetFileListForFileManager>(Input));

            PagingData = qData.Item1;
            Data = _Mapper.Map<List<vmCompo_ModalFileList>>(qData.Item2);

            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_ModalFileList Input { get; set; }
        public List<vmCompo_ModalFileList> Data { get; set; }
        public OutPagingData PagingData { get; set; }
    }
}
