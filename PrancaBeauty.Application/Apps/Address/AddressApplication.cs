using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Address;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Application.Exceptions;
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
        private readonly IAddressRepository _AddressRepository;
        public AddressApplication(IAddressRepository addressRepository, ILogger logger, ILocalizer localizer)
        {
            _AddressRepository = addressRepository;
            _Logger = logger;
            _Localizer = localizer;
        }

        public async Task<(OutPagingData, List<OutGetAddressByUserIdForManage>)> GetAddressByUserIdForManageAsync(string UserId, string LangId, string Search, int PageNum, int Take)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserId))
                    throw new ArgumentInvalidException($"'{nameof(UserId)}' cannot be null or whitespace.");

                if (string.IsNullOrWhiteSpace(LangId))
                    throw new ArgumentInvalidException($"'{nameof(LangId)}' cannot be null or whitespace.");

                var qData = _AddressRepository.Get
                                              .Where(a => a.UserId == Guid.Parse(UserId))
                                              .Select(a => new OutGetAddressByUserIdForManage
                                              {
                                                  Id = a.Id.ToString(),
                                                  Address = a.tblCountries.tblCountries_Translates.Where(b => b.LangId == Guid.Parse(LangId)).Select(b => b.Title).Single() + "- " +
                                                             a.tblProvinces.tblProvinces_Translate.Where(b => b.LangId == Guid.Parse(LangId)).Select(b => b.Title).Single() + "- " +
                                                             a.tblCities.tblCities_Translates.Where(b => b.LangId == Guid.Parse(LangId)).Select(b => b.Title).Single() + "- " +
                                                             a.District + "- " +
                                                             _Localizer["Plaque"] + " " + a.Plaque + "- " +
                                                             _Localizer["Unit"] + " " + a.Unit + "- " +
                                                             a.FirstName + " " + a.LastName,
                                                  CountBills = 0,
                                                  Date = a.Date
                                              })
                                              .Where(a => Search != null ? a.Address.Contains(Search) : true)
                                              .OrderByDescending(a => a.Date);

                // صفحه بندی داده ها
                var qPagingData = PagingData.Calc(await qData.LongCountAsync(), PageNum, Take);

                return (qPagingData, await qData.Skip((int)qPagingData.Skip).Take(qPagingData.Take).ToListAsync());
            }
            catch (ArgumentInvalidException)
            {
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
                if (Input is null)
                    throw new ArgumentInvalidException("Input cant e null.");

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
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> RemoveAddressAsync(string UserId, string Id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserId))
                    throw new ArgumentInvalidException($"'{nameof(UserId)}' cannot be null or whitespace.");

                if (string.IsNullOrWhiteSpace(Id))
                    throw new ArgumentInvalidException($"'{nameof(Id)}' cannot be null or whitespace.");

                var qData = await _AddressRepository.Get
                                                   .Where(a => a.Id == Guid.Parse(Id))
                                                   .Where(a => a.UserId == Guid.Parse(UserId))
                                                   //.Where(a=> a.tblBills.Count()==0)
                                                   .SingleOrDefaultAsync();

                if (qData == null)
                    return new OperationResult().Failed("AddressHasFactors");

                await _AddressRepository.DeleteAsync(qData, default);

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OutGetAddressDetails> GetAddressDetailsAsync(string UserId, string Id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserId))
                    throw new ArgumentInvalidException($"'{nameof(UserId)}' cannot be null or whitespace.");

                if (string.IsNullOrWhiteSpace(Id))
                    throw new ArgumentInvalidException($"'{nameof(Id)}' cannot be null or whitespace.");

                var qData = await _AddressRepository.Get
                                                   .Where(a => a.Id == Guid.Parse(Id))
                                                   .Where(a => a.UserId == Guid.Parse(UserId))
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
                if (Input is null)
                    throw new ArgumentInvalidException("Input cant be null.");

                var qData = await _AddressRepository.Get
                                                   .Where(a => a.Id == Guid.Parse(Input.Id))
                                                   .Where(a => a.UserId == Guid.Parse(Input.UserId))
                                                   .SingleOrDefaultAsync();

                if (qData == null)
                    return new OperationResult().Failed("IdIsInvalid");

                qData.Address = Input.Address;
                qData.CityId =Guid.Parse( Input.CityId);
                qData.ProviceId = Guid.Parse(Input.ProvinceId);
                qData.CountryId = Guid.Parse(Input.CountryId);
                qData.District = Input.District;
                qData.FirstName = Input.FirstName;
                qData.LastName = Input.LastName;
                qData.NationalCode = Input.NationalCode;
                qData.PhoneNumber = Input.PhoneNumber;
                qData.Plaque = Input.Plaque;
                qData.PostalCode = Input.PostalCode;
                qData.Unit = Input.Unit;

                await _AddressRepository.UpdateAsync(qData, default);

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
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
