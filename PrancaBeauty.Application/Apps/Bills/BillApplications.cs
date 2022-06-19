using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Domain.Enums;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.Carts;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Bills;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Cart;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Domin.Bills.BillAgg.Contracts;
using PrancaBeauty.Domin.Bills.BillAgg.Entities;
using PrancaBeauty.Domin.Bills.BillItemsAgg.Entities;
using PrancaBeauty.Domin.PostalBarcodes.PostalBarcodeAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Bills
{
    public class BillApplication : IBillApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IBillsRepository _BillRepository;
        private readonly ICartApplication _CartApplication;
        public BillApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, ICartApplication cartApplication, IBillsRepository billRepository)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _CartApplication=cartApplication;
            _BillRepository=billRepository;
        }

        private string CreateBillNumberAsync()
        {
            return DateTime.Now.ToString("yyMMddHHmm")+new Random().Next(100, 999);
        }

        public async Task<OperationResult> CreateBillFromCartAsync(InpCreateBillFromCart Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region واکشی سبد خرید کاربر
                List<OutGetItemsForBill> LstCart;
                {
                    LstCart = await _CartApplication.GetItemsForBillAsync(new InpGetItemsForBill { UserId=Input.UserId });
                    if (LstCart==null)
                        return new OperationResult().Failed("Error500");
                }
                #endregion

                #region ایجاد صورت حساب پرداخت نشده
                string _BillId = "";
                string _BillNumber = CreateBillNumberAsync();
                {
                    _BillId = new Guid().SequentialGuid().ToString();
                    tblBills tBill = new tblBills
                    {
                        Id= _BillId.ToGuid(),
                        UserId=Input.UserId.ToGuid(),
                        AddressId=null,
                        GateId=null,
                        Date= DateTime.Now,
                        BillNumber=_BillNumber,
                        GateNumber=null,
                        TransactionNumber=null,
                        TaxAmount=null,
                        TotalPrice=null,
                        Status=BillStatusEnum.NotPayyed,
                        tblPostalBarcodes= LstCart.GroupBy(a => a.SellerId)
                                                  .Select(a => new tblPostalBarcodes
                                                  {
                                                      Id= new Guid().SequentialGuid(),
                                                      ShippingMethodId=null,
                                                      Barcode="",
                                                      ChangeStatusDateTime=DateTime.Now,
                                                      Date= DateTime.Now,
                                                      Height=0,
                                                      Width=0,
                                                      Length=0,
                                                      Weight=0,
                                                      Price=0,
                                                      Tax=0,
                                                      TotalPrice=0,
                                                      Status=PostalBarcodeEnum.InCheckingOut,
                                                      tblBillItems= a.Select(b => new tblBillItems
                                                      {
                                                          Id= new Guid().SequentialGuid(),
                                                          BillId=_BillId.ToGuid(),
                                                          ProductId=b.ProductId.ToGuid(),
                                                          SellerId=a.Key.ToGuid(),
                                                          VarianrItemId=b.VarianrItemId.ToGuid(),
                                                          TaxGroupId=b.TaxGroupId.ToGuid(),
                                                          Price=null,
                                                          TotalPrice=null,
                                                          TaxPercent=null,
                                                          Qty=b.Qty,
                                                          ReasonReturn="",
                                                          ReturnStatus=BillItemStatusEnum.None
                                                      }).ToList()
                                                  }).ToList(),

                    };

                    await _BillRepository.AddAsync(tBill, default, true);
                }
                #endregion

                #region خالی کردن سبد خرید
                {
                    var _Result = await _CartApplication.ClearCartAsync(new InpClearCart
                    {
                        UserId=Input.UserId
                    });
                    if (!_Result.IsSucceeded)
                        return new OperationResult().Failed(_Result.Message);
                }
                #endregion

                return new OperationResult().Succeeded(_BillNumber);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");

            }
        }

        public async Task<OutGetBillDetails> GetBillDetailsAsync(InpGetBillDetails Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await (from a in _BillRepository.Get
                                   where a.BillNumber==Input.BillNumber
                                   where Input.SellerUserId!=null ? a.tblPostalBarcodes.Any(b => b.tblBillItems.Any(c => c.SellerId==Input.SellerUserId.ToGuid())) : true
                                   where Input.BuyerUserId !=null ? a.UserId==Input.BuyerUserId.ToGuid() : true
                                   select new OutGetBillDetails
                                   {
                                       AddressId=a.AddressId!=null ? a.AddressId.ToString() : null,
                                       BillStatus=a.Status,
                                       GateId=a.GateId!=null ? a.GateId.ToString() : null,
                                       Note=a.Note,
                                       ShippingAmount=a.Status==BillStatusEnum.Payyed ? a.tblPostalBarcodes.Sum(a => a.TotalPrice) : null,
                                       TaxAmount= a.TaxAmount,
                                       TotalPrice= a.TotalPrice,
                                       TransactionNumber=a.TransactionNumber,
                                       LstSellerGroups= (from b in a.tblPostalBarcodes
                                                         let Items = b.tblBillItems
                                                         let Seller = Items.First().tblSellers
                                                         where Input.SellerUserId!=null ? Items.Any(c => c.SellerId==Input.SellerUserId.ToGuid()) : true
                                                         select new OutGetBillDetailsItemGroups
                                                         {
                                                             SellerName=Seller.Name,
                                                             Barcode= b.Barcode,
                                                             ChangeStatusDateTime=b.ChangeStatusDateTime,
                                                             ShippingAmount=a.Status==BillStatusEnum.Payyed ? b.TotalPrice : 0,
                                                             ShippingMethodId=b.ShippingMethodId!=null ? b.ShippingMethodId.ToString() : null,
                                                             ShippingStatus=b.Status,
                                                             LstItems= (from c in Items
                                                                        let CurrencySymbol = c.tblProducts.tblProductPrices.Where(b => b.IsActive && b.CurrencyId==Input.CurrencyId.ToGuid()).Select(b => b.tblCurrency.Symbol).Single()
                                                                        let Price = c.tblProducts.tblProductPrices.Where(a => a.CurrencyId==Input.CurrencyId.ToGuid() && a.IsActive).Select(a => a.Price).Single()
                                                                        let SellerPercent = c.tblProductVariantItems.Percent
                                                                        let PercentSavePrice = c.tblProductVariantItems.tblProductDiscounts!=null ? c.tblProductVariantItems.tblProductDiscounts.Percent : 0
                                                                        let OldPrice = Price + ((Price/100)*SellerPercent)
                                                                        let NewPrice = OldPrice - ((OldPrice/100)*PercentSavePrice)
                                                                        let TaxPercent = c.tblProducts.tblTaxGroups.Percent
                                                                        let TaxAmount = (NewPrice/100)*TaxPercent
                                                                        let TotalPrice = NewPrice * c.Qty
                                                                        let Product = c.tblProducts
                                                                        let VariantItem = c.tblProductVariantItems
                                                                        select new OutGetBillDetailsItems
                                                                        {
                                                                            Title=Product.Title,
                                                                            Name=Product.Name,
                                                                            VariantTopic=VariantItem.tblProductVariants.tblProductVariants_Translates.Where(a => a.LangId==Input.LangId.ToGuid()).Select(a => a.Title).Single(),
                                                                            VariantValue=VariantItem.Title,
                                                                            Qty=c.Qty,
                                                                            TotalAmount=a.Status==BillStatusEnum.Payyed ? c.TotalPrice.Value : TotalPrice,
                                                                            TaxAmount=a.Status==BillStatusEnum.Payyed ? (c.TotalPrice.Value/100)*c.TaxPercent.Value : TaxAmount,
                                                                            CurrencySymbol=CurrencySymbol
                                                                        }).ToList()
                                                         }).ToList()
                                   })
                                   .SingleOrDefaultAsync();

                if (qData==null)
                    return null;

                qData.TotalPrice= qData.TotalPrice!=null ? qData.TotalPrice.Value : qData.LstSellerGroups.Sum(b => b.LstItems.Sum(c => c.TotalAmount));
                qData.TaxAmount= qData.TaxAmount!=null ? qData.TaxAmount.Value : qData.LstSellerGroups.Sum(b => b.LstItems.Sum(c => c.TaxAmount));
                qData.ShippingAmount= qData.ShippingAmount!=null ? qData.ShippingAmount.Value : 0; // TODO: Get ShippingAmount from webService;

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;

            }
        }

        public async Task<(OutPagingData PagingData, List<OutGetListBillForManage>)> GetListBillForManageAsync(InpGetListBillForManage Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = from a in _BillRepository.Get
                            where Input.BillNumber!=null ? a.BillNumber==Input.BillNumber : true
                            where Input.SellerUserId !=null ? a.tblPostalBarcodes.Any(b => b.tblBillItems.Any(c => c.SellerId==Input.SellerUserId.ToGuid())) : true
                            where Input.BuyerUserId !=null ? a.UserId==Input.BuyerUserId.ToGuid() : true
                            let Address = a.tblAddress
                            select new OutGetListBillForManage
                            {
                                Id=a.Id.ToString(),
                                UserFullName=a.tblUsers.FirstName  +" "+ a.tblUsers.LastName,
                                BillNumber=a.BillNumber,
                                TransactionNumber=a.TransactionNumber??"",
                                Status=a.Status,
                                Date=a.Date,
                                GateTitle=a.tblPaymentGates!=null ? a.tblPaymentGates.tblPaymentGateTranslate.Where(a => a.LangId==Input.LangId.ToGuid()).Select(a => a.Title).Single() : "",
                                Address=Address!=null ? _Localizer["Country"] +": " + Address.tblCountries.tblCountries_Translates.Where(a => a.LangId==Input.LangId.ToGuid()).Select(a => a.Title).Single()
                                                       + "- "+_Localizer["Province"] + ": " + Address.tblProvinces.tblProvinces_Translate.Where(a => a.LangId==Input.LangId.ToGuid()).Select(a => a.Title).Single()
                                                       + "- "+_Localizer["City"] + ": " + Address.tblCities.tblCities_Translates.Where(a => a.LangId==Input.LangId.ToGuid()).Select(a => a.Title).Single()
                                                       + "- "+_Localizer["District"] + ": " + Address.District
                                                       + "- "+_Localizer["Plaque"] + ": " + Address.Plaque
                                                       + "- "+_Localizer["Unit"] + ": " + Address.Unit
                                                       + "- "+_Localizer["FullName"] + ": " + Address.FirstName+" "+Address.LastName : ""
                                                       + "- "+_Localizer["PostalCode"] + ": " + Address.PostalCode
                                                       + "- " + _Localizer["Phone"] + ": " + Address.PhoneNumber
                            };

                #region Paging
                var _PagingData = PagingData.Calc(await qData.CountAsync(), Input.Page, Input.Take);
                #endregion

                return (_PagingData, await qData.OrderByDescending(a => a.Status).ThenByDescending(a => a.Date).Skip((int)_PagingData.Skip).Take(Input.Take).ToListAsync());
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;

            }
        }
    }
}
