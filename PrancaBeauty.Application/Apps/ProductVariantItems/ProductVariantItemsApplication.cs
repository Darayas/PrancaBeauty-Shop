using Framework.Exceptions;
using Framework.Infrastructure;
using PrancaBeauty.Application.Contracts.ProductVariantItems;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Product.ProductVariantItemsAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductVariantItems
{
    public class ProductVariantItemsApplication : IProductVariantItemsApplication
    {
        private readonly ILogger _Logger;
        private readonly IProductVariantItemsRepository _ProductVariantItemsRepository;

        public ProductVariantItemsApplication(IProductVariantItemsRepository productVariantItemsRepository, ILogger logger)
        {
            _ProductVariantItemsRepository = productVariantItemsRepository;
            _Logger = logger;
        }

        public async Task<OperationResult> AddVariantsToProductAsync(InpAddVariantsToProduct Input)
        {
            try
            {
                #region Validations
                if (Input is null)
                    throw new ArgumentInvalidException($"{nameof(Input)} cant be null.");

                List<ValidationResult> _ValidationResult = null;
                if (Validator.TryValidateObject(Input, new ValidationContext(Input), _ValidationResult))
                    throw new ArgumentInvalidException(string.Join(",", _ValidationResult.Select(a => a.ErrorMessage)));
                #endregion


            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }
    }
}
