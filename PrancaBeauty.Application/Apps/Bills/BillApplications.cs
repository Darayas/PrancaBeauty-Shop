using Framework.Common.ExMethods;
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

                var qData = await _BillRepository.Get
                                        .Where(a => a.BillNumber==Input.BillNumber)
                                        .Select(a => new OutGetBillDetails
                                        {
                                            AddressId=a.AddressId!=null ? a.AddressId.ToString() : null,
                                            BillStatus=a.Status,
                                            GateId=a.GateId!=null ? a.GateId.ToString() : null,
                                            Note=a.Note,
                                            ShippingAmount=0,
                                            TaxAmount=a.TaxAmount!=null ? a.TaxAmount.Value : 0,
                                            TotalPrice=a.TotalPrice!=null ? a.TotalPrice.Value : 0,
                                            TransactionNumber=a.TransactionNumber,
                                            LstSellerGroups=null
                                        })
                                        .SingleOrDefaultAsync();

                if (qData==null)
                    return null;

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
    }
}
