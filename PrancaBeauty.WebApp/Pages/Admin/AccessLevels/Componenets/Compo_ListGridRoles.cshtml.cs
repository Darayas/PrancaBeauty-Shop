using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Roles;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Roles;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.WebApp.Pages.Admin.AccessLevels.Componenets
{
    public class Compo_ListGridRolesModel : PageModel
    {
        private readonly IRoleApplication _RoleApplication;
        public Compo_ListGridRolesModel(IRoleApplication roleApplication)
        {
            _RoleApplication = roleApplication;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Input.AccessLevelId != null)
            {
                // ویرایش
                var RolesList = await _RoleApplication.ListOfRolesByAccessLevelIdAsync(new InpListOfRolesByAccessLevelId { AccessLevelId = Input.AccessLevelId });

                if (RolesList == null)
                    throw new Exception("RolesList is null.");

                ViewData["SelectedRoles"] = RolesList;
                return Page();
            }
            else
            {

                return Page();
            }
        }

        public async Task<JsonResult> OnPostReadAsync([DataSourceRequest] DataSourceRequest request, string ParentId = null)
        {
            var qData = (await _RoleApplication.ListOfRolesAsync(new InpListOfRoles { ParentId = ParentId }))
                                               .Select(a => new vmCompo_ListGridRolesModel
                                               {
                                                   Id = a.Id,
                                                   Name = a.Name,
                                                   Description = a.Description,
                                                   HasChild = a.HasChild,
                                                   PageName = a.PageName,
                                                   ParentId = a.ParentId,
                                                   Sort = a.Sort
                                               });

            return new JsonResult(await qData.ToDataSourceResultAsync(request));
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_ListGridRolesModel Input { get; set; }
    }
}
