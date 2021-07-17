using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Countries;
using PrancaBeauty.Domin.Region.CountryAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Countries
{
    public class CountryApplication : ICountryApplication
    {
        private readonly ILogger _Logger;
        private readonly ICountryRepository _CountryRepository;

        public CountryApplication(ICountryRepository countryRepository, ILogger logger)
        {
            _CountryRepository = countryRepository;
            _Logger = logger;
        }

        public async Task<List<OutGetListForCombo>> GetListForComboAsync(string LangId, string Search)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(LangId))
                    throw new ArgumentInvalidException($"'{nameof(LangId)}' cannot be null or whitespace.");

                var qData = await _CountryRepository.Get
                                                   .Where(a => a.IsActive)
                                                   .Select(a => new OutGetListForCombo
                                                   {
                                                       Id = a.Id.ToString(),
                                                       Name = a.Name,
                                                       Title = a.tblCountries_Translates.Where(b => b.LangId == Guid.Parse(LangId)).Select(b => b.Title).Single(),
                                                       FlagUrl = a.tblFiles.tblFileServer.HttpDomin +
                                                                  a.tblFiles.tblFileServer.HttpPath +
                                                                  a.tblFiles.Path +
                                                                  a.tblFiles.FileName
                                                   })
                                                   .Where(a => Search != null ? a.Title.Contains(Search) : true)
                                                   .ToListAsync();

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
