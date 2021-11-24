using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Contracts.Products;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;

namespace PrancaBeauty.WebApp.Pages.User.Products
{
    [Authorize(Roles = Roles.CanViewListProducts)]
    public class ListModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly IProductApplication _ProductApplication;

        public ListModel(IProductApplication productApplication, IMapper mapper)
        {
            _ProductApplication = productApplication;
            _Mapper = mapper;
        }

        public IActionResult OnGet(string LangId)
        {
            if (Input.LangId == null)
                Input.LangId = LangId;

            return Page();
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request, string LangId)
        {
            if (!User.IsInRole(Roles.CanViewListAllAuthorUserProducts))
            {
                if (!User.IsInRole(Roles.CanViewListAllSellerUserProducts))
                {
                    Input.SellerUserId = User.GetUserDetails().UserId;
                    Input.AuthorUserId = null;
                }
                else
                {
                    Input.AuthorUserId = User.GetUserDetails().UserId;
                }
            }

            if (Input.LangId == null)
                Input.LangId = LangId;

            var qData = await _ProductApplication.GetProductsForManageAsync(new InpGetProductsForManage()
            {
                Page = request.Page,
                Take = request.PageSize,
                LangId = Input.LangId,
                SellerUserId = Input.SellerUserId,
                AuthorUserId = Input.AuthorUserId,
                Title = Input.Title,
                Name = Input.Name,
                IsDelete = Input.IsDelete,
                IsDraft = Input.IsDraft,
                IsConfirmed = Input.IsConfirmed,
                IsSchedule = Input.IsSchedule
            });

            // Mapping
            var Items = _Mapper.Map<List<vmProductList>>(qData.Item2);

            // To DataSourceResult
            var _DataGrid = Items.ToDataSourceResult(request);

            // Paging
            _DataGrid.Total = (int)qData.Item1.CountAllItem;
            _DataGrid.Data = Items;

            return new JsonResult(_DataGrid);
        }

        [BindProperty(SupportsGet = true)]
        public viProductList Input { get; set; }
    }
}
