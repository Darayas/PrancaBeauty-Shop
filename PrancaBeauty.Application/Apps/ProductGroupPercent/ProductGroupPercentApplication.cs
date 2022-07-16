using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductGroupPercent;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Products;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Domin.Product.ProductGroupPercentAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductGroupPercent
{
    public class ProductGroupPercentApplication : IProductGroupPercentApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductApplication _ProductApplication;
        private readonly IProductGroupPercentRepository _ProductGroupPercentRepository;

        public ProductGroupPercentApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, IProductGroupPercentRepository productGroupPercentRepository, IProductApplication productApplication)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _ProductGroupPercentRepository=productGroupPercentRepository;
            _ProductApplication=productApplication;
        }

        public async Task<OperationResult<List<OutGetProductGroupPercents>>> GetProductGroupPercentsAsync(InpGetProductGroupPercents Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get Product Group Id
                string _ProductGroupId;
                {
                    var qGroupResult = await _ProductApplication.GetProductGroupIdAsync(new InpGetProductGroupId
                    {
                        ProductId=Input.ProductId
                    });

                    if (!qGroupResult.IsSucceeded)
                        return new OperationResult<List<OutGetProductGroupPercents>>().Failed(qGroupResult.Message);

                    _ProductGroupId=qGroupResult.Data;
                }
                #endregion

                var qData = await _ProductGroupPercentRepository.Get
                                            .Where(a => a.ProductGroupId==_ProductGroupId.ToGuid())
                                            .Select(a => new OutGetProductGroupPercents
                                            {
                                                WalletId=a.WalletId.ToString(),
                                                Title=a.Title,
                                                Percent=a.Percent
                                            })
                                            .ToListAsync();

                return new OperationResult<List<OutGetProductGroupPercents>>().Succeeded(qData);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<List<OutGetProductGroupPercents>>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<List<OutGetProductGroupPercents>>().Failed("Error500");
            }
        }
    }
}
