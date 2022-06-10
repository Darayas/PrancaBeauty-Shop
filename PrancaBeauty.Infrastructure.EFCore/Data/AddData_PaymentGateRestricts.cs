using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Entities;
using PrancaBeauty.Domin.PaymentGate.PaymentGateRestrictAgg.Entities;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using PrancaBeauty.Domin.Region.CurrnencyAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_PaymentGateRestricts
    {
        BaseRepository<tblPaymentGateRestricts> _RepPaymentGateRestricts;
        BaseRepository<tblPaymentGates> _RepPaymentGates;
        BaseRepository<tblCountries> _RepCountries;
        BaseRepository<tblCurrencies> _RepCurrencies;
        public AddData_PaymentGateRestricts()
        {
            _RepPaymentGateRestricts= new BaseRepository<tblPaymentGateRestricts>(new MainContext());
            _RepCountries= new BaseRepository<tblCountries>(new MainContext());
            _RepCurrencies= new BaseRepository<tblCurrencies>(new MainContext());
            _RepPaymentGates= new BaseRepository<tblPaymentGates>(new MainContext());
        }

        public void Run()
        {
            var _RialCurrencyId = _RepCurrencies.Get.Where(a => a.Name=="Rial").Select(a => a.Id).Single();
            var _DollarCurrencyId = _RepCurrencies.Get.Where(a => a.Name=="Dollars").Select(a => a.Id).Single();
            var _IranCountryId = _RepCountries.Get.Where(a => a.Name=="Iran").Select(a => a.Id).Single();
            var _USACountryId = _RepCountries.Get.Where(a => a.Name=="USA").Select(a => a.Id).Single();

            #region ZarinPal
            {
                var _GateId = _RepPaymentGates.Get.Where(a => a.Name=="ZarinPal").Select(a => a.Id).Single();

                if (!_RepPaymentGateRestricts.Get.Where(a => a.PaymentGateId==_GateId).Where(a => a.CountryId==_IranCountryId && a.CurrencyId==_RialCurrencyId).Any())
                {
                    _RepPaymentGateRestricts.AddAsync(new tblPaymentGateRestricts
                    {
                        Id= new Guid().SequentialGuid(),
                        PaymentGateId=_GateId,
                        CountryId=_IranCountryId,
                        CurrencyId=_RialCurrencyId,
                        IsEnable=true
                    }, default, true).Wait();
                }
            }
            #endregion

            #region Paypal
            {
                var _GateId = _RepPaymentGates.Get.Where(a => a.Name=="Paypal").Select(a => a.Id).Single();

                if (!_RepPaymentGateRestricts.Get.Where(a => a.PaymentGateId==_GateId).Where(a => a.CountryId==_IranCountryId && a.CurrencyId==_RialCurrencyId).Any())
                {
                    _RepPaymentGateRestricts.AddAsync(new tblPaymentGateRestricts
                    {
                        Id= new Guid().SequentialGuid(),
                        PaymentGateId=_GateId,
                        CountryId=_USACountryId,
                        CurrencyId=_DollarCurrencyId,
                        IsEnable=true
                    }, default, true).Wait();
                }
            }
            #endregion
        }
    }
}
