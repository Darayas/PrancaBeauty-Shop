using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Domain.Enums;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Address;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Domin.Users.AddressAgg.Contracts;
using PrancaBeauty.Domin.Users.AddressAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Address
{
    public class AddressApplication : IAddressApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IAddressRepository _AddressRepository;
        public AddressApplication(IAddressRepository addressRepository, ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _AddressRepository = addressRepository;
            _Logger = logger;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<(OutPagingData, List<OutGetAddressByUserIdForManage>)> GetAddressByUserIdForManageAsync(InpGetAddressByUserIdForManage Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = _AddressRepository.Get
                                              .Where(a => a.UserId == Guid.Parse(Input.UserId))
                                              .Select(a => new OutGetAddressByUserIdForManage
                                              {
                                                  Id = a.Id.ToString(),
                                                  Address = a.tblCountries.tblCountries_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single() + "- " +
                                                             a.tblProvinces.tblProvinces_Translate.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single() + "- " +
                                                             a.tblCities.tblCities_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single() + "- " +
                                                             a.District + "- " +
                                                             _Localizer["Plaque"] + " " + a.Plaque + "- " +
                                                             _Localizer["Unit"] + " " + a.Unit + "- " +
                                                             a.FirstName + " " + a.LastName,
                                                  CountBills = a.tblBills.Where(a => a.Status==BillStatusEnum.Payyed).Count(),
                                                  Date = a.Date
                                              })
                                              .Where(a => Input.Search != null ? a.Address.Contains(Input.Search) : true)
                                              .OrderByDescending(a => a.Date);

                // صفحه بندی داده ها
                var qPagingData = PagingData.Calc(await qData.LongCountAsync(), Input.PageNum, Input.Take);

                return (qPagingData, await qData.Skip((int)qPagingData.Skip).Take(qPagingData.Take).ToListAsync());
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return (null, null);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return (null, null);
            }
        }

        public async Task<OperationResult> AddAddressAsync(InpAddAddress Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                await _AddressRepository.AddAsync(new tblAddress()
                {
                    Id = new Guid().SequentialGuid(),
                    UserId = Guid.Parse(Input.UserId),
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    PhoneNumber = Input.PhoneNumber,
                    PostalCode = Input.PostalCode,
                    Address = Input.Address,
                    CityId = Guid.Parse(Input.CityId),
                    ProviceId = Guid.Parse(Input.ProvinceId),
                    CountryId = Guid.Parse(Input.CountryId),
                    Date = DateTime.Now,
                    District = Input.District,
                    NationalCode = Input.NationalCode,
                    Plaque = Input.Plaque,
                    Unit = Input.Unit
                }, default, true);

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

        public async Task<OperationResult> RemoveAddressAsync(InpRemoveAddress Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _AddressRepository.Get
                                                   .Where(a => a.Id == Guid.Parse(Input.Id))
                                                   .Where(a => a.UserId == Guid.Parse(Input.UserId))
                                                   .Select(a => new
                                                   {
                                                       Address = a,
                                                       HasBill = a.tblBills.Where(a => a.Status==BillStatusEnum.Payyed).Any()
                                                   })
                                                   .SingleOrDefaultAsync();

                if (qData == null)
                    return new OperationResult().Failed("AddressNotFound");

                if (qData.HasBill)
                    return new OperationResult().Failed("AddressHasBill");

                await _AddressRepository.DeleteAsync(qData.Address, default);

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

        public async Task<OutGetAddressDetails> GetAddressDetailsAsync(InpGetAddressDetails Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _AddressRepository.Get
                                                   .Where(a => a.Id == Guid.Parse(Input.Id))
                                                   .Where(a => a.UserId == Guid.Parse(Input.UserId))
                                                   .Select(a => new OutGetAddressDetails
                                                   {
                                                       Id = a.Id.ToString(),
                                                       UserId = a.UserId.ToString(),
                                                       CityId = a.CityId.ToString(),
                                                       CountryId = a.CountryId.ToString(),
                                                       Address = a.Address,
                                                       District = a.District,
                                                       FirstName = a.FirstName,
                                                       LastName = a.LastName,
                                                       NationalCode = a.NationalCode,
                                                       PhoneNumber = a.PhoneNumber,
                                                       Plaque = a.Plaque,
                                                       PostalCode = a.PostalCode,
                                                       ProvinceId = a.ProviceId.ToString(),
                                                       Unit = a.Unit
                                                   })
                                                   .SingleOrDefaultAsync();


                return qData;
            }
            catch (ArgumentInvalidException)
            {
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<OperationResult> EditAddressAsync(InpEditAddress Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _AddressRepository.Get
                                                   .Where(a => a.Id == Guid.Parse(Input.Id))
                                                   .Where(a => a.UserId == Guid.Parse(Input.UserId))
                                                   .Select(a => new
                                                   {
                                                       tAddress = a,
                                                       HasBill= a.tblBills.Where(a => a.Status==BillStatusEnum.Payyed).Any()
                                                   })
                                                   .SingleOrDefaultAsync();

                if (qData == null)
                    return new OperationResult().Failed("IdIsInvalid");

                if(qData.HasBill)
                    return new OperationResult().Failed("AddressHasFactors");

                qData.tAddress.Address = Input.Address;
                qData.tAddress.CityId =Guid.Parse(Input.CityId);
                qData.tAddress.ProviceId = Guid.Parse(Input.ProvinceId);
                qData.tAddress.CountryId = Guid.Parse(Input.CountryId);
                qData.tAddress.District = Input.District;
                qData.tAddress.FirstName = Input.FirstName;
                qData.tAddress.LastName = Input.LastName;
                qData.tAddress.NationalCode = Input.NationalCode;
                qData.tAddress.PhoneNumber = Input.PhoneNumber;
                qData.tAddress.Plaque = Input.Plaque;
                qData.tAddress.PostalCode = Input.PostalCode;
                qData.tAddress.Unit = Input.Unit;

                await _AddressRepository.UpdateAsync(qData.tAddress, default);

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
    }
}
