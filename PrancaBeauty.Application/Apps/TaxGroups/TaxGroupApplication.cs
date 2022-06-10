using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductTopics;
using PrancaBeauty.Application.Contracts.ApplicationDTO.TaxGroup;
using PrancaBeauty.Domin.Bills.TaxAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductTopicAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.TaxGroups
{
    public class TaxGroupApplication : ITaxGroupApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ITaxGroupRepository _TaxGroupRepository;

        public TaxGroupApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, ITaxGroupRepository taxGroupRepository)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _TaxGroupRepository=taxGroupRepository;
        }

        public async Task<List<OutGetListTaxGroupForCombo>> GetListTaxGroupForComboAsync(InpGetListTaxGroupForCombo Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _TaxGroupRepository.Get
                                                     .Where(a=>a.CountryId==Input.CountryId.ToGuid())  
                                                     .Select(a => new OutGetListTaxGroupForCombo()
                                                     {
                                                         Id = a.Id.ToString(),
                                                         Title = a.Title,
                                                         Percent=a.Percent
                                                     })
                                                     .Where(a => Input.Text != null ? a.Title.Contains(Input.Text) : true)
                                                     .OrderBy(a => a.Title)
                                                     .Skip(0)
                                                     .Take(50)
                                                     .ToListAsync();

                if (qData == null)
                    return null;

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
