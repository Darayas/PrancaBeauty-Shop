using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Currency;
using PrancaBeauty.Domin.Region.CurrnencyAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Currency
{
    public class CurrencyApplication : ICurrencyApplication
    {
        private readonly ILogger _Logger;
        private readonly ICurrencyRepository _CurrencyRepository;

        public CurrencyApplication(ICurrencyRepository currencyRepository, ILogger logger)
        {
            _CurrencyRepository = currencyRepository;
            _Logger = logger;
        }

        public async Task<OutGetMainByCountryId> GetMainByCountryIdAsync(string LangId, string CountryId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(LangId))
                    throw new ArgumentInvalidException($"'{nameof(LangId)}' cannot be null or whitespace.");

                if (string.IsNullOrWhiteSpace(CountryId))
                    throw new ArgumentInvalidException($"'{nameof(CountryId)}' cannot be null or whitespace.");

                var qData = await _CurrencyRepository.Get
                                                     .Where(a => a.CountryId == Guid.Parse(CountryId))
                                                     .Where(a => a.IsDefault == true)
                                                     .Select(a => new OutGetMainByCountryId
                                                     {
                                                         Id = a.Id.ToString(),
                                                         Symbol = a.Symbol,
                                                         Title = a.tblCurrency_Translates.Where(b => b.LangId == Guid.Parse(LangId)).Select(b => b.Title).Single()
                                                     })
                                                     .SingleOrDefaultAsync();

                if (qData == null)
                    return null;

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
    }
}
