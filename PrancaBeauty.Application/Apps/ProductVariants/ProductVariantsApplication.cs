using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ProductVariants;
using PrancaBeauty.Domin.Product.ProductVariantAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductVariants
{
    public class ProductVariantsApplication : IProductVariantsApplication
    {
        private readonly ILogger _Logger;
        private readonly IProductVariantsRepository _ProductVariantsRepository;

        public ProductVariantsApplication(IProductVariantsRepository productVariantsRepository, ILogger logger)
        {
            _ProductVariantsRepository = productVariantsRepository;
            _Logger = logger;
        }

        public async Task<List<outGetLstForCombo>> GetLstForComboAsync(string LangId, string Text)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(LangId))
                    throw new ArgumentInvalidException($"'{nameof(LangId)}' cannot be null or whitespace.");

                var qData = await _ProductVariantsRepository.Get
                                                           .Select(a => new outGetLstForCombo
                                                           {
                                                               Id = a.Id.ToString(),
                                                               Title = a.tblProductVariants_Translates.Where(b => b.LangId == Guid.Parse(LangId)).Select(b => b.Title).Single()
                                                           })
                                                           .Where(a => Text != null ? a.Title.Contains(Text) : true)
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
