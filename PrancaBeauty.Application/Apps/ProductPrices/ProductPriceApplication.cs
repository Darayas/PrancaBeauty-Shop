using AutoMapper.Configuration.Annotations;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using PrancaBeauty.Application.Contracts.ProductPrice;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Product.ProductPricesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductPricesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductPrices
{
    public class ProductPriceApplication : IProductPriceApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductPricesRepository _ProductPricesRepository;

        public ProductPriceApplication(IProductPricesRepository productPricesRepository, ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _ProductPricesRepository = productPricesRepository;
            _Logger = logger;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<OperationResult> AddPriceToProductAsyc(InpAddPriceToProduct Input)
        {
            try
            {
                #region Validations
                 Input.CheckModelState(_ServiceProvider);
                #endregion

                var tProductPrice = new tblProductPrices
                {
                    Id = new Guid().SequentialGuid(),
                    ProductId = Guid.Parse(Input.ProductId),
                    UserId = Guid.Parse(Input.UserId),
                    CurrencyId = Guid.Parse(Input.CurrencyId),
                    Date = DateTime.Now,
                    IsActive = true,
                    Price = Input.Price.GetValidPrice()
                };

                await _ProductPricesRepository.AddAsync(tProductPrice, default, true);

                return new OperationResult().Succeeded();
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
