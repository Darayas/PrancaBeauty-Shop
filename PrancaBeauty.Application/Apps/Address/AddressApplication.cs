﻿using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Address;
using PrancaBeauty.Application.Exceptions;
using PrancaBeauty.Domin.Users.AddressAgg.Contracts;
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

        public async Task<(OutPagingData, List<GetAddressByUserIdForManage>)> GetAddressByUserIdForManageAsync(string UserId, string LangId, string Search, int PageNum, int Take)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserId))
                    throw new ArgumentInvalidException($"'{nameof(UserId)}' cannot be null or whitespace.");

                if (string.IsNullOrWhiteSpace(LangId))
                    throw new ArgumentInvalidException($"'{nameof(LangId)}' cannot be null or whitespace.");

                if (PageNum < 1)
                    throw new ArgumentInvalidException("PageNum < 1");

                if (Take < 1)
                    throw new ArgumentInvalidException("Take < 1");

                var qData = _AddressRepository.Get
                                              .Where(a => a.UserId == Guid.Parse(UserId))
                                              .Select(a => new GetAddressByUserIdForManage
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
    }
}
