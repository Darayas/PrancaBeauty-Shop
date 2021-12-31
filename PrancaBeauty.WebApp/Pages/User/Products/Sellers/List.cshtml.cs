

using Framework.Common.ExMethods;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Apps.ProductSellers;
using PrancaBeauty.Application.Contracts.Products;
using PrancaBeauty.Application.Contracts.ProductSellers;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.User.Products.Sellers
{
    [Authorize(Roles = Roles.CanViewListProductSellerList)]
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

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request, string Text,string ProductId)
        {
            string UserId = User.GetUserDetails().UserId;
            if (User.IsInRole(Roles.CanViewListProductSellerListAllUser))
                UserId = null;

            var qData = await _ProductSellersApplication.GetAllSellerForManageByProductIdAsync(new InpGetAllSellerForManageByProductId
            {
                FullName= Text,
                Page = request.Page,
                Take = request.PageSize,
                ProductId= ProductId,
                UserId= UserId
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

        public string ProductId { get; set; }
        public string ProductTitle { get; set; }
    }
}
