using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Domain.Enums;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.Carts;
using PrancaBeauty.Application.Apps.PaymentGates;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Bills;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Cart;
using PrancaBeauty.Application.Contracts.ApplicationDTO.PaymentGate;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Domin.Bills.BillAgg.Contracts;
using PrancaBeauty.Domin.Bills.BillAgg.Entities;
using PrancaBeauty.Domin.Bills.BillItemsAgg.Entities;
using PrancaBeauty.Domin.PostalBarcodes.PostalBarcodeAgg.Entities;
using PrancaBeauty.Infrastructure.PaymentGates;
using PrancaBeauty.Infrastructure.PaymentGates.Contracts;
using PrancaBeauty.Infrastructure.PaymentGates.ZarinPal;
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
        private readonly IPaymentGateApplication _PaymentGateApplication;
        public BillApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, ICartApplication cartApplication, IBillsRepository billRepository, IPaymentGateApplication paymentGateApplication)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _CartApplication=cartApplication;
            _BillRepository=billRepository;
            _PaymentGateApplication=paymentGateApplication;
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
                    {
                        return new OperationResult().Failed("Error500");
                    }
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
                        Authority=null,
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
                    {
                        return new OperationResult().Failed(_Result.Message);
                    }
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
                                       Id=a.Id.ToString(),
                                       BuyerUserId=a.UserId.ToString(),
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
                                                             Id=b.Id.ToString(),
                                                             SellerName=Seller.Name,
                                                             Barcode= b.Barcode,
                                                             SellerAddressId=Seller.AddressId.ToString(),
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
                {
                    return null;
                }

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
                                TransactionNumber=a.TransactionNumber,
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

        public async Task<OperationResult> ChangeBillAddressAsync(InpChangeBillAddress Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _BillRepository.Get
                                        .Where(a => a.Id==Input.BillId.ToGuid())
                                        .Where(a => a.UserId==Input.BuyerUserId.ToGuid())
                                        .Where(a => a.Status==BillStatusEnum.NotPayyed)
                                        .SingleOrDefaultAsync();

                if (qData==null)
                {
                    return new OperationResult().Failed("IdNotFound");
                }

                qData.AddressId=Input.AddressId.ToGuid();

                await _BillRepository.UpdateAsync(qData, default, true);

                return new OperationResult().Succeeded();
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

        public async Task<OperationResult> ChangeBillPaymentGateAsync(InpChangeBillPaymentGate Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _BillRepository.Get
                                        .Where(a => a.Id==Input.BillId.ToGuid())
                                        .Where(a => a.UserId==Input.BuyerUserId.ToGuid())
                                        .Where(a => a.Status==BillStatusEnum.NotPayyed)
                                        .SingleOrDefaultAsync();

                if (qData==null)
                {
                    return new OperationResult().Failed("IdNotFound");
                }

                qData.GateId=Input.GateId.ToGuid();

                await _BillRepository.UpdateAsync(qData, default, true);

                return new OperationResult().Succeeded();
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

        public async Task<OperationResult> ChangeBillAuthorityAsync(InpChangeBillAuthority Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _BillRepository.Get
                                        .Where(a => a.Id==Input.BillId.ToGuid())
                                        .Where(a => a.UserId==Input.BuyerUserId.ToGuid())
                                        .Where(a => a.Status==BillStatusEnum.NotPayyed)
                                        .SingleOrDefaultAsync();

                if (qData==null)
                    return new OperationResult().Failed("IdNotFound");

                qData.Authority=Input.Authority;

                await _BillRepository.UpdateAsync(qData, default, true);

                return new OperationResult().Succeeded();
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

        public async Task<OperationResult> ChangeBillStatusToPaymentedAsync(InpChangeBillStatusToPaymented Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _BillRepository.Get
                                        .Where(a => a.Id==Input.BillId.ToGuid())
                                        .Where(a => a.UserId==Input.BuyerUserId.ToGuid())
                                        .Where(a => a.Status==BillStatusEnum.NotPayyed)
                                        .SingleOrDefaultAsync();

                if (qData==null)
                    return new OperationResult().Failed("IdNotFound");

                qData.TransactionNumber=Input.TransactionNumber;
                qData.Status=BillStatusEnum.Payyed;
                qData.Date=DateTime.Now;

                await _BillRepository.UpdateAsync(qData, default, true);

                return new OperationResult().Succeeded();
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

        public async Task<OutGetBillDetailsForPayment> GetBillDetailsForPaymentAsync(InpGetBillDetailsForPayment Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var q = await (from a in _BillRepository.Get
                               where a.BillNumber==Input.BillNumber
                               select new
                               {
                                   Id = a.Id.ToString(),
                                   IsPayyed = a.Status==BillStatusEnum.Payyed,
                                   Email = a.tblUsers.Email,
                                   Mobile = a.tblUsers.PhoneNumber,
                                   GateName = a.GateId != null ? a.tblPaymentGates.Name : null,
                                   Amounts = (from b in a.tblPostalBarcodes
                                              select new
                                              {
                                                  ShippingAmount = b.TotalPrice,
                                                  Prices = from c in b.tblBillItems
                                                           let Price = c.tblProducts.tblProductPrices.Where(a => a.CurrencyId==Input.CurrencyId.ToGuid() && a.IsActive).Select(a => a.Price).Single()
                                                           let SellerPercent = c.tblProductVariantItems.Percent
                                                           let PercentSavePrice = c.tblProductVariantItems.tblProductDiscounts!=null ? c.tblProductVariantItems.tblProductDiscounts.Percent : 0
                                                           let OldPrice = Price + ((Price/100)*SellerPercent)
                                                           let NewPrice = OldPrice - ((OldPrice/100)*PercentSavePrice)
                                                           let TaxPercent = c.tblProducts.tblTaxGroups.Percent
                                                           let TaxAmount = (NewPrice/100)*TaxPercent
                                                           let TotalPrice = NewPrice * c.Qty
                                                           select TotalPrice
                                              })
                               }).ToListAsync();

                var qData = q.Select(a => new OutGetBillDetailsForPayment
                {
                    Id=a.Id,
                    Email=a.Email,
                    Mobile=a.Mobile,
                    GateName=a.GateName,
                    IsPayyed=a.IsPayyed,
                    Amount=a.Amounts.Select(b => b.ShippingAmount+b.Prices.Sum()).Sum()
                }).SingleOrDefault();

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<OperationResult<OutStartPayment>> StartPaymentAsync(InpStartPayment Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get bill data
                OutGetBillDetailsForPayment _BillData;
                {
                    var qBill = await GetBillDetailsForPaymentAsync(new InpGetBillDetailsForPayment
                    {
                        BillNumber=Input.BillNumber,
                        UserId=Input.UserId,
                        CurrencyId=Input.CurrencyId
                    });

                    if (qBill==null)
                    {
                        return new OperationResult<OutStartPayment>().Failed("BillNotFound");
                    }

                    if (qBill.GateName==null)
                    {
                        return new OperationResult<OutStartPayment>().Failed("PleaseSelectGate");
                    }

                    _BillData = qBill;
                }
                #endregion

                #region Register payment gate
                IPayment _Payment = null;
                {
                    var _GateData = await _PaymentGateApplication.GetGateDataAsync(new InpGetGateData { GateName=_BillData.GateName });
                    if (_GateData.IsSucceeded==false)
                    {
                        return new OperationResult<OutStartPayment>().Failed(_GateData.Message);
                    }

                    if (_BillData.GateName.ToLower()=="ZarinPal".ToLower())
                    {
                        _Payment= new ZarinPalGate(_ServiceProvider, _Logger, _GateData.Data.EncryptedData);
                    }
                }
                #endregion

                #region Start Pay
                string _Authority;
                string _PaymentyGateUrl;
                {
                    var _Response = await _Payment.StartPaymentAsync(new InpStartPay
                    {
                        Email=_BillData.Email,
                        Mobile=_BillData.Mobile,
                        Description="",
                        //Amount=_BillData.Amount,
                        Amount=2000,
                        CallBackUrl=Input.CallBackUrl
                    });
                    if (_Response.StatusCode!=100)
                        return new OperationResult<OutStartPayment>().Failed("SelectedPaymentGateNotRespond");

                    _Authority= _Response.Authority;
                    _PaymentyGateUrl= _Response.PaymentyGateUrl;
                }
                #endregion

                #region Chane bill authority
                {
                    var _Result = await ChangeBillAuthorityAsync(new InpChangeBillAuthority
                    {
                        BillId=_BillData.Id,
                        BuyerUserId=Input.UserId,
                        Authority=_Authority
                    });
                    if (_Result.IsSucceeded==false)
                        return new OperationResult<OutStartPayment>().Failed(_Result.Message);
                }
                #endregion

                return new OperationResult<OutStartPayment>().Succeeded(new OutStartPayment { PaymentyGateUrl= _PaymentyGateUrl });
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<OutStartPayment>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<OutStartPayment>().Failed("Error500");
            }
        }

        public async Task<OperationResult<OutPaymentVeryfication>> PaymentVeryficationAsync(InpPaymentVeryfication Input)
        {

            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get bill data
                OutGetBillDetailsForPayment _BillData;
                {
                    var qBill = await GetBillDetailsForPaymentAsync(new InpGetBillDetailsForPayment
                    {
                        BillNumber=Input.BillNumber,
                        UserId=Input.UserId,
                        CurrencyId=Input.CurrencyId
                    });

                    if (qBill==null)
                    {
                        return new OperationResult<OutPaymentVeryfication>().Failed("BillNotFound");
                    }

                    if (qBill.GateName==null)
                    {
                        return new OperationResult<OutPaymentVeryfication>().Failed("PleaseSelectGate");
                    }

                    _BillData = qBill;
                }
                #endregion

                #region Register payment gate
                IPayment _Payment = null;
                {
                    var _GateData = await _PaymentGateApplication.GetGateDataAsync(new InpGetGateData { GateName=_BillData.GateName });
                    if (_GateData.IsSucceeded==false)
                    {
                        return new OperationResult<OutPaymentVeryfication>().Failed(_GateData.Message);
                    }

                    if (_BillData.GateName.ToLower()=="ZarinPal".ToLower())
                    {
                        _Payment= new ZarinPalGate(_ServiceProvider, _Logger, _GateData.Data.EncryptedData);
                    }
                }
                #endregion

                #region Payment Veryfication 
                string _TransactionNumber = null;
                {
                    var _Response = await _Payment.PaymentVaryficationAsync(new InpPayVaryfication
                    {
                        Amount= _BillData.Amount,
                        QueryData=Input.QueryData
                    });

                    if (_Response.StatusCode!=100)
                        return new OperationResult<OutPaymentVeryfication>().Failed("PaymentVeryficationFaild");

                    _TransactionNumber= _Response.TransactionNumber;
                }
                #endregion

                #region Final price registeration
                {

                }
                #endregion

                #region Recharge wallets
                {

                }
                #endregion

                #region Change bill status to paymented
                {
                    var _Result = await ChangeBillStatusToPaymentedAsync(new InpChangeBillStatusToPaymented
                    {
                        BillId=_BillData.Id,
                        BuyerUserId=Input.UserId,
                        TransactionNumber=_TransactionNumber
                    });
                    if (_Result.IsSucceeded==false)
                        return new OperationResult<OutPaymentVeryfication>().Failed(_Localizer["PaymentVeryficationFaild.PleaseContactToAdmin", _TransactionNumber]);
                }
                #endregion

                return new OperationResult<OutPaymentVeryfication>().Succeeded(new OutPaymentVeryfication { TransactionNumber=_TransactionNumber });
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<OutPaymentVeryfication>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<OutPaymentVeryfication>().Failed("Error500");
            }
        }
    }
}
