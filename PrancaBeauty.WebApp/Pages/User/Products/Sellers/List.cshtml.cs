using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.WebApp.Models.ViewInput;
using System.Threading.Tasks;
using PrancaBeauty.Application.Contracts.Products;
using System.Net;
using System.Globalization;
using PrancaBeauty.WebApp.Authentication;
using Framework.Common.ExMethods;
using System;
using Kendo.Mvc.UI;
using PrancaBeauty.Application.Apps.ProductSellers;

namespace PrancaBeauty.WebApp.Pages.User.Products.Sellers
{
    [Authorize(Roles =Roles.CanViewListProductSellerList)]
    public class ListModel : PageModel
    {
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductApplication _ProductApplication;
        private readonly IProductSellersApplication _ProductSellersApplication;

        public ListModel(IServiceProvider serviceProvider, IProductApplication productApplication, IProductSellersApplication productSellersApplication)
        {
            _ServiceProvider = serviceProvider;
            _ProductApplication = productApplication;
            _ProductSellersApplication = productSellersApplication;
        }

        public async Task<IActionResult> OnGetAsync(viGetListSellers Input, string ReturnUrl = null)
        {
            Input.CheckModelState(_ServiceProvider);

            ProductId = Input.ProductId;
            ProductTitle = await _ProductApplication.GetTitleByIdAsync(new InpGetTitleById { ProductId = Input.ProductId });
            ViewData["ReturnUrl"] = WebUtility.UrlDecode(ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/User/Products/List");

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


            

            // To DataSourceResult
            var _DataGrid = Items.ToDataSourceResult(request);

            // Paging
            _DataGrid.Total = (int)qData.Item1.CountAllItem;
            _DataGrid.Data = Items;

            return new JsonResult(_DataGrid);
        }

        public string ProductId { get; set; }
        public string ProductTitle { get; set; }
    }
}
