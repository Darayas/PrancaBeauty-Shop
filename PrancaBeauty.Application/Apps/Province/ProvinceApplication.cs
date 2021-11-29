using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Province;
using PrancaBeauty.Domin.Region.ProvinceAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Province
{
    public class ProvinceApplication : IProvinceApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProvinceRepository _ProvinceRepository;
        public ProvinceApplication(IProvinceRepository provinceRepository, ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _ProvinceRepository = provinceRepository;
            _Logger = logger;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<List<OutGetListForCombo>> GetListForComboAsync(InpGetListForCombo Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProvinceRepository.Get
                                                     .Where(a => a.IsActive)
                                                     .Where(a => a.CountryId == Guid.Parse(Input.CountryId))
                                                     .Select(a => new OutGetListForCombo
                                                     {
                                                         Id = a.Id.ToString(),
                                                         Name = a.Name,
                                                         Title = a.tblProvinces_Translate.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single()
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
