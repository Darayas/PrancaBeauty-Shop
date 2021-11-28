using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.City;
using PrancaBeauty.Domin.Region.CityAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Cities
{
    public class CityApplication : ICityApplication
    {
        private ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly ICityRepository _CityRepository;

        public CityApplication(ICityRepository cityRepository, ILogger logger, ILocalizer localizer)
        {
            _CityRepository = cityRepository;
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

                var qData = await _CityRepository.Get
                                                .Where(a => a.IsActive)
                                                .Where(a => a.ProvinceId == Guid.Parse(Input.ProvinceId))
                                                .Select(a => new OutGetListForCombo
                                                {
                                                    Id = a.Id.ToString(),
                                                    Name = a.Name,
                                                    Title = a.tblCities_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single()
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
