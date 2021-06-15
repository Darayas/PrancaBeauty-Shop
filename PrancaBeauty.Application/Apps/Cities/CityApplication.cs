using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.City;
using PrancaBeauty.Application.Exceptions;
using PrancaBeauty.Domin.Region.CityAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Cities
{
    public class CityApplication : ICityApplication
    {
        private ILogger _Logger;
        private readonly ICityRepository _CityRepository;

        public CityApplication(ICityRepository cityRepository, ILogger logger)
        {
            _CityRepository = cityRepository;
            _Logger = logger;
        }


        public async Task<List<OutGetListForCombo>> GetListForComboAsync(string LangId, string ProvinceId, string Search)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(LangId))
                    throw new ArgumentInvalidException($"'{nameof(LangId)}' cannot be null or whitespace.");

                if (string.IsNullOrWhiteSpace(ProvinceId))
                    throw new ArgumentInvalidException($"'{nameof(ProvinceId)}' cannot be null or whitespace.");

                var qData = await _CityRepository.Get
                                                .Where(a => a.IsActive)
                                                .Where(a => a.ProvinceId == Guid.Parse(ProvinceId))
                                                .Select(a => new OutGetListForCombo
                                                {
                                                    Id = a.Id.ToString(),
                                                    Name = a.Name,
                                                    Title = a.tblCities_Translates.Where(b => b.LangId == Guid.Parse(LangId)).Select(b => b.Title).Single()
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
