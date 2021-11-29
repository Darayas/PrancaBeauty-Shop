using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ProductVariants;
using PrancaBeauty.Domin.Product.ProductVariantAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductVariants
{
    public class ProductVariantsApplication : IProductVariantsApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductVariantsRepository _ProductVariantsRepository;

        public ProductVariantsApplication(IProductVariantsRepository productVariantsRepository, ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _ProductVariantsRepository = productVariantsRepository;
            _Logger = logger;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<List<outGetLstForCombo>> GetLstForComboAsync(InpGetLstForCombo Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductVariantsRepository.Get
                                                           .Select(a => new outGetLstForCombo
                                                           {
                                                               Id = a.Id.ToString(),
                                                               Title = a.tblProductVariants_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single()
                                                           })
                                                           .Where(a => Input.Text != null ? a.Title.Contains(Input.Text) : true)
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
