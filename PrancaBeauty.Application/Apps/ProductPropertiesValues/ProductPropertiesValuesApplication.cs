using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.ProductPropertis;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductProperties;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductPropertiesValues;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Domin.Product.ProductPropertiesValuesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductPropertiesValuesAgg.Entities;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductPropertiesValues
{
    public class ProductPropertiesValuesApplication : IProductPropertiesValuesApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductPropertiesValuesRepository _ProductPropertiesValuesRepository;
        private readonly IProductPropertisApplication _ProductPropertisApplication;
        public ProductPropertiesValuesApplication(IProductPropertiesValuesRepository productPropertiesValuesRepository, ILogger logger, IProductPropertisApplication productPropertisApplication, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _ProductPropertiesValuesRepository = productPropertiesValuesRepository;
            _Logger = logger;
            _ProductPropertisApplication = productPropertisApplication;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<OperationResult> AddPropertiesToProductAsync(InpAddPropertiesToProduct Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);

                if (Input.PropItems is null)
                    throw new ArgumentInvalidException($"'{nameof(Input.PropItems)}' cannot be null.");

                if (Input.PropItems.Count() == 0)
                    throw new ArgumentInvalidException($"'{nameof(Input.PropItems)}' count must be greater than zero.");

                #endregion

                foreach (var item in Input.PropItems.Where(a => a.Value != null || a.Value != ""))
                {
                    if (await _ProductPropertisApplication.CheckExistByIdAsync(new InpCheckExistById { Id = item.Id }))
                        await _ProductPropertiesValuesRepository.AddAsync(new tblProductPropertiesValues()
                        {
                            Id = new Guid().SequentialGuid(),
                            ProductId = Guid.Parse(Input.ProductId),
                            ProductPropertiesId = Guid.Parse(item.Id),
                            Value = item.Value
                        }, default, false);
                }

                await _ProductPropertiesValuesRepository.SaveChangeAsync();

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

        public async Task<OperationResult> RemovePropertiesByProductIdAsync(InpRemovePropertiesByProductId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductPropertiesValuesRepository.Get.Where(a => a.ProductId == Guid.Parse(Input.ProductId)).ToListAsync();

                await _ProductPropertiesValuesRepository.DeleteRangeAsync(qData, default, true);

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

        public async Task<OperationResult> EditProductPropertiesAsync(InpEditProductProperties Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region حذف خصوصیات قبلی
                {
                    var _Result = await RemovePropertiesByProductIdAsync(new InpRemovePropertiesByProductId
                    {
                        ProductId = Input.ProductId
                    });
                    if (_Result.IsSucceeded == false)
                    {
                        return new OperationResult().Failed(_Result.Message);
                    }
                }
                #endregion

                #region افزودن خصوصیات جدید
                {
                    var _Result = await AddPropertiesToProductAsync(new InpAddPropertiesToProduct
                    {
                        ProductId = Input.ProductId,
                        PropItems = Input.PropItems.Select(a => new InpAddPropertiesToProduct_Items
                        {
                            Id = a.Id,
                            Value = a.Value
                        }).ToList()
                    });
                    if (_Result.IsSucceeded == false)
                    {
                        return new OperationResult().Failed(_Result.Message);
                    }
                }
                #endregion

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
