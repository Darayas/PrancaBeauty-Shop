using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Countries;
using PrancaBeauty.Domin.Region.CountryAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Countries
{
    public class CountryApplication : ICountryApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly ICountryRepository _CountryRepository;

        public CountryApplication(ICountryRepository countryRepository, ILogger logger, ILocalizer localizer)
        {
            _CountryRepository = countryRepository;
            _Logger = logger;
            _Localizer = localizer;
        }

        public async Task<List<OutGetListForCombo>> GetListForComboAsync(InpGetListForCombo Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_Localizer);
                #endregion

                var qData = await _CountryRepository.Get
                                                   .Where(a => a.IsActive)
                                                   .Select(a => new OutGetListForCombo
                                                   {
                                                       Id = a.Id.ToString(),
                                                       Name = a.Name,
                                                       Title = a.tblCountries_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single(),
                                                       FlagUrl = a.tblFiles.tblFilePaths.tblFileServer.HttpDomin +
                                                                  a.tblFiles.tblFilePaths.tblFileServer.HttpPath +
                                                                  a.tblFiles.tblFilePaths.Path +
                                                                  a.tblFiles.FileName
                                                   })
                                                   .Where(a => Input.Search != null ? a.Title.Contains(Input.Search) : true)
                                                   .ToListAsync();

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
    }
}
