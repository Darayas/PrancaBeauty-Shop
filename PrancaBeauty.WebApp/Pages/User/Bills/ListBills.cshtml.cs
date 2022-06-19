using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Bills;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Bills;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Products;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Sellers;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Types;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.User.Bills
{
    [Authorize]
    public class ListBillsModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IBillApplication _BillApplication;

        public ListBillsModel(ILogger logger, IMapper mapper, IServiceProvider serviceProvider, IBillApplication billApplication, IMsgBox msgBox, ILocalizer localizer)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ServiceProvider=serviceProvider;
            _BillApplication=billApplication;
            _MsgBox=msgBox;
            _Localizer=localizer;
        }

        public IActionResult OnGet(string LangId)
        {
            ViewData["LangId"]=LangId;
            return Page();
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request, string LangId)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Check Access
                {
                    if (User.IsInRole(Roles.CanViewListBillAdmin))
                    {

                    }
                    else if (User.IsInRole(Roles.CanViewListBillSeller))
                    {
                        Input.SellerUserId=User.GetUserDetails().SellerId;
                    }
                    else
                    {
                        Input.SellerUserId=null;
                        Input.BuyerUserId=User.GetUserDetails().UserId;
                    }
                }
                #endregion

                var qData = await _BillApplication.GetListBillForManageAsync(new InpGetListBillForManage()
                {
                    Page = request.Page,
                    Take = request.PageSize,
                    LangId = LangId,
                    BillNumber = Input.BillNumber,
                    BuyerUserId = Input.BuyerUserId,
                    SellerUserId=Input.SellerUserId
                });

                // Mapping
                var Items = _Mapper.Map<List<vmListBills>>(qData.Item2);

                // To DataSourceResult
                var _DataGrid = Items.ToDataSourceResult(request);

                // Paging
                _DataGrid.Total = (int)qData.Item1.CountAllItem;
                _DataGrid.Data = Items;

                return new JsonResult(_DataGrid);
            }
            catch (ArgumentInvalidException)
            {
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return StatusCode(500);
            }
           
        }

        public async Task<IActionResult> OnPostCreateBillAsync(viCreateBill Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                string _UserId = User.GetUserDetails().UserId;

                var _Result = await _BillApplication.CreateBillFromCartAsync(new InpCreateBillFromCart
                {
                    UserId=_UserId
                });
                if (_Result.IsSucceeded)
                    return new JsResult($"location.href='/{CultureInfo.CurrentCulture.Parent.Name}/User/Bill/{_Result.Message}'");
                else
                    return _MsgBox.FaildMsg(_Localizer[_Result.Message]);
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FaildMsg(_Localizer["Error500"]);
            }
        }

        [BindProperty(SupportsGet = true)]
        public viListBills Input { get; set; }

    }
}
