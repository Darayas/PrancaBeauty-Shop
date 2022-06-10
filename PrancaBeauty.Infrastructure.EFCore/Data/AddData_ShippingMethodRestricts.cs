using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.Region.CityAgg.Entities;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using PrancaBeauty.Domin.Region.ProvinceAgg.Entities;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodAgg.Entities;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodRestrictAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_ShippingMethodRestricts
    {
        BaseRepository<tblShippingMethodRestricts> _RepShippingMethodRestricts;
        BaseRepository<tblShippingMethods> _RepShippingMethods;
        BaseRepository<tblCountries> _RepCountries;
        BaseRepository<tblProvinces> _RepProvinces;
        BaseRepository<tblCities> _RepCities;
        public AddData_ShippingMethodRestricts()
        {
            _RepShippingMethodRestricts= new BaseRepository<tblShippingMethodRestricts>(new MainContext());
            _RepShippingMethods= new BaseRepository<tblShippingMethods>(new MainContext());
            _RepCountries= new BaseRepository<tblCountries>(new MainContext());
            _RepProvinces= new BaseRepository<tblProvinces>(new MainContext());
            _RepCities= new BaseRepository<tblCities>(new MainContext());
        }

        public void Run()
        {

            #region Country: Iran - Iran post service
            {
                var _ShippingMethodId = _RepShippingMethods.Get.Where(a => a.Name=="IranPostService").Select(a => a.Id).Single();
                var _CountryId = _RepCountries.Get.Where(a => a.Name=="Iran").Select(a => a.Id).Single();

                #region Province: Tehran
                {
                    var _ProvinceId = _RepProvinces.Get.Where(a => a.Name=="Tehran").Select(a => a.Id).Single();

                    #region City: Tehran
                    {
                        var _CityId = _RepCities.Get.Where(a => a.Name=="Tehran").Select(a => a.Id).Single();
                        if (!_RepShippingMethodRestricts.Get.Where(a => a.ShippingMethodId==_ShippingMethodId).Where(a => a.CountryId==_CountryId && a.ProvinceId==_ProvinceId && a.CityId==_CityId).Any())
                        {
                            _RepShippingMethodRestricts.AddAsync(new tblShippingMethodRestricts
                            {
                                Id= new Guid().SequentialGuid(),
                                ShippingMethodId=_ShippingMethodId,
                                CountryId=_CountryId,
                                ProvinceId=_ProvinceId,
                                CityId=_CityId,
                                IsActive=true,
                            }, default, true).Wait();
                        }
                    }
                    #endregion
                }
                #endregion

                #region Province: Fars
                {
                    var _ProvinceId = _RepProvinces.Get.Where(a => a.Name=="Fars").Select(a => a.Id).Single();

                    #region City: Fars
                    {
                        var _CityId = _RepCities.Get.Where(a => a.Name=="Shiraz").Select(a => a.Id).Single();
                        if (!_RepShippingMethodRestricts.Get.Where(a => a.ShippingMethodId==_ShippingMethodId).Where(a => a.CountryId==_CountryId && a.ProvinceId==_ProvinceId && a.CityId==_CityId).Any())
                        {
                            _RepShippingMethodRestricts.AddAsync(new tblShippingMethodRestricts
                            {
                                Id= new Guid().SequentialGuid(),
                                ShippingMethodId=_ShippingMethodId,
                                CountryId=_CountryId,
                                ProvinceId=_ProvinceId,
                                CityId=_CityId,
                                IsActive=true,
                            }, default, true).Wait();
                        }
                    }
                    #endregion
                }
                #endregion

                #region Province: Esfahan
                {
                    var _ProvinceId = _RepProvinces.Get.Where(a => a.Name=="Esfahan").Select(a => a.Id).Single();

                    #region City: Esfahan
                    {
                        var _CityId = _RepCities.Get.Where(a => a.Name=="Esfahan").Select(a => a.Id).Single();
                        if (!_RepShippingMethodRestricts.Get.Where(a => a.ShippingMethodId==_ShippingMethodId).Where(a => a.CountryId==_CountryId && a.ProvinceId==_ProvinceId && a.CityId==_CityId).Any())
                        {
                            _RepShippingMethodRestricts.AddAsync(new tblShippingMethodRestricts
                            {
                                Id= new Guid().SequentialGuid(),
                                ShippingMethodId=_ShippingMethodId,
                                CountryId=_CountryId,
                                ProvinceId=_ProvinceId,
                                CityId=_CityId,
                                IsActive=true,
                            }, default, true).Wait();
                        }
                    }
                    #endregion
                }
                #endregion
            }
            #endregion

            #region Country: Iran - Tipax
            {
                var _ShippingMethodId = _RepShippingMethods.Get.Where(a => a.Name=="Tipax").Select(a => a.Id).Single();
                var _CountryId = _RepCountries.Get.Where(a => a.Name=="Iran").Select(a => a.Id).Single();

                #region Province: Tehran
                {
                    var _ProvinceId = _RepProvinces.Get.Where(a => a.Name=="Tehran").Select(a => a.Id).Single();

                    #region City: Tehran
                    {
                        var _CityId = _RepCities.Get.Where(a => a.Name=="Tehran").Select(a => a.Id).Single();
                        if (!_RepShippingMethodRestricts.Get.Where(a => a.ShippingMethodId==_ShippingMethodId).Where(a => a.CountryId==_CountryId && a.ProvinceId==_ProvinceId && a.CityId==_CityId).Any())
                        {
                            _RepShippingMethodRestricts.AddAsync(new tblShippingMethodRestricts
                            {
                                Id= new Guid().SequentialGuid(),
                                ShippingMethodId=_ShippingMethodId,
                                CountryId=_CountryId,
                                ProvinceId=_ProvinceId,
                                CityId=_CityId,
                                IsActive=true,
                            }, default, true).Wait();
                        }
                    }
                    #endregion
                }
                #endregion

                #region Province: Fars
                {
                    var _ProvinceId = _RepProvinces.Get.Where(a => a.Name=="Fars").Select(a => a.Id).Single();

                    #region City: Fars
                    {
                        var _CityId = _RepCities.Get.Where(a => a.Name=="Shiraz").Select(a => a.Id).Single();
                        if (!_RepShippingMethodRestricts.Get.Where(a => a.ShippingMethodId==_ShippingMethodId).Where(a => a.CountryId==_CountryId && a.ProvinceId==_ProvinceId && a.CityId==_CityId).Any())
                        {
                            _RepShippingMethodRestricts.AddAsync(new tblShippingMethodRestricts
                            {
                                Id= new Guid().SequentialGuid(),
                                ShippingMethodId=_ShippingMethodId,
                                CountryId=_CountryId,
                                ProvinceId=_ProvinceId,
                                CityId=_CityId,
                                IsActive=true,
                            }, default, true).Wait();
                        }
                    }
                    #endregion
                }
                #endregion

                #region Province: Esfahan
                {
                    var _ProvinceId = _RepProvinces.Get.Where(a => a.Name=="Esfahan").Select(a => a.Id).Single();

                    #region City: Esfahan
                    {
                        var _CityId = _RepCities.Get.Where(a => a.Name=="Esfahan").Select(a => a.Id).Single();
                        if (!_RepShippingMethodRestricts.Get.Where(a => a.ShippingMethodId==_ShippingMethodId).Where(a => a.CountryId==_CountryId && a.ProvinceId==_ProvinceId && a.CityId==_CityId).Any())
                        {
                            _RepShippingMethodRestricts.AddAsync(new tblShippingMethodRestricts
                            {
                                Id= new Guid().SequentialGuid(),
                                ShippingMethodId=_ShippingMethodId,
                                CountryId=_CountryId,
                                ProvinceId=_ProvinceId,
                                CityId=_CityId,
                                IsActive=true,
                            }, default, true).Wait();
                        }
                    }
                    #endregion
                }
                #endregion
            }
            #endregion
        }
    }
}
