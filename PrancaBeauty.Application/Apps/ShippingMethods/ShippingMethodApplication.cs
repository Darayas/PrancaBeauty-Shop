using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.Address;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Address;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShippingMethods;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ShippingMethods
{
    public class ShippingMethodApplication : IShippingMethodApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShippingMethodRepository _ShippingMethodRepository;
        private readonly IAddressApplication _AddressApplication;
        public ShippingMethodApplication(ILogger logger, IServiceProvider serviceProvider, IShippingMethodRepository shippingMethodRepository, IAddressApplication addressApplication)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _ShippingMethodRepository=shippingMethodRepository;
            _AddressApplication=addressApplication;
        }

        public async Task<List<OutGetShippingMethodForBill>> GetShippingMethodForBillAsync(InpGetShippingMethodForBill Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get buyer address
                string _BuyerCounrtyId = null;
                string _BuyerProvinceId = null;
                string _BuyerCityId = null;
                {
                    var qAddress = await _AddressApplication.GetAddressByIdAsync(new InpGetAddressById { AddressId=Input.BuyerAddressId });
                    if (qAddress==null)
                        return null;

                    _BuyerCounrtyId=qAddress.CountryId;
                    _BuyerProvinceId=qAddress.ProviceId;
                    _BuyerCityId=qAddress.CityId;
                }
                #endregion

                #region Get seller address
                string _SellerCounrtyId = null;
                string _SellerProvinceId = null;
                string _SellerCityId = null;
                {
                    var qAddress = await _AddressApplication.GetAddressByIdAsync(new InpGetAddressById { AddressId=Input.SellerAddressId });
                    if (qAddress==null)
                        return null;

                    _SellerCounrtyId=qAddress.CountryId;
                    _SellerProvinceId=qAddress.ProviceId;
                    _SellerCityId=qAddress.CityId;
                }
                #endregion

                var qData = await _ShippingMethodRepository.Get
                                    .Where(a => a.tblShippingMethodRestricts.Any(b => b.CountryId==_BuyerCounrtyId.ToGuid() && b.CountryId==_SellerCounrtyId.ToGuid()))
                                    .Where(a => a.tblShippingMethodRestricts.Any(b => b.ProvinceId==_BuyerProvinceId.ToGuid() && b.ProvinceId==_SellerProvinceId.ToGuid()))
                                    .Where(a => a.tblShippingMethodRestricts.Any(b => b.CityId==_BuyerCityId.ToGuid() && b.CityId==_SellerCityId.ToGuid()))
                                    .Select(a => new OutGetShippingMethodForBill
                                    {
                                        Id=a.Id.ToString(),
                                        Title=a.tblShippingMethodTranslate.Where(b=>b.LangId==Input.LangId.ToGuid()).Select(b=>b.Title).Single()
                                    })
                                    .ToListAsync();

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
