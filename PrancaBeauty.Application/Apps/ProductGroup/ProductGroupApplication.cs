using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductGroups;
using PrancaBeauty.Application.Contracts.ApplicationDTO.TaxGroup;
using PrancaBeauty.Domin.Bills.TaxAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductGroupAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductGroup
{
    public class ProductGroupApplication : IProductGroupApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductGroupRepository _ProductGroupRepository;

        public ProductGroupApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, IProductGroupRepository productGroupRepository)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _ProductGroupRepository=productGroupRepository;
        }

        public async Task<List<OutGetListProductGroupForCombo>> GetListProductGroupForComboAsync(InpGetListProductGroupForCombo Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductGroupRepository.Get
                                                     .Where(a => a.TopicId==Input.TopicId.ToGuid())
                                                     .Select(a => new OutGetListProductGroupForCombo()
                                                     {
                                                         Id = a.Id.ToString(),
                                                         Title = a.tblProductGroupTranslate.Where(a => a.LangId==Input.LangId.ToGuid()).Select(a => a.Title).Single(),
                                                         Percent=a.tblProductGroupPercents.Sum(a => a.Percent)
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
