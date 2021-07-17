using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Province;
using PrancaBeauty.Domin.Region.ProvinceAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Province
{
    public class ProvinceApplication : IProvinceApplication
    {
        private readonly ILogger _Logger;
        private readonly IProvinceRepository _ProvinceRepository;
        public ProvinceApplication(IProvinceRepository provinceRepository, ILogger logger)
        {
            _ProvinceRepository = provinceRepository;
            _Logger = logger;
        }

        public async Task<List<OutGetListForCombo>> GetListForComboAsync(string LangId, string CountryId, string Search)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(LangId))
                    throw new ArgumentInvalidException($"'{nameof(LangId)}' cannot be null or whitespace.");

                if (string.IsNullOrWhiteSpace(CountryId))
                    throw new ArgumentInvalidException($"'{nameof(CountryId)}' cannot be null or whitespace.");

                var qData = await _ProvinceRepository.Get
                                                     .Where(a => a.IsActive)
                                                     .Where(a => a.CountryId == Guid.Parse(CountryId))
                                                     .Select(a => new OutGetListForCombo
                                                     {
                                                         Id = a.Id.ToString(),
                                                         Name = a.Name,
                                                         Title = a.tblProvinces_Translate.Where(b => b.LangId == Guid.Parse(LangId)).Select(b => b.Title).Single()
                                                     })
                                                     .Where(a => Search != null ? a.Title.Contains(Search) : true)
                                                     .ToListAsync();

                return qData;
            }
            catch (ArgumentInvalidException ex)
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
