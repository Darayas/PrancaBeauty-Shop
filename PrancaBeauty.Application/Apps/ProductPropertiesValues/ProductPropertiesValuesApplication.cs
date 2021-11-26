using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.ProductPropertis;
using PrancaBeauty.Application.Contracts.ProductProperties;
using PrancaBeauty.Application.Contracts.ProductPropertiesValues;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Product.ProductPropertiesValuesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductPropertiesValuesAgg.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductPropertiesValues
{
    public class ProductPropertiesValuesApplication : IProductPropertiesValuesApplication
    {
        private readonly ILogger _Logger;
        private readonly IProductPropertiesValuesRepository _ProductPropertiesValuesRepository;
        private readonly IProductPropertisApplication _ProductPropertisApplication;
        public ProductPropertiesValuesApplication(IProductPropertiesValuesRepository productPropertiesValuesRepository, ILogger logger, IProductPropertisApplication productPropertisApplication)
        {
            _ProductPropertiesValuesRepository = productPropertiesValuesRepository;
            _Logger = logger;
            _ProductPropertisApplication = productPropertisApplication;
        }

        public async Task<OperationResult> AddPropertiesToProductAsync(InpAddPropertiesToProduct Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState();

                if (Input.PropItems is null)
                    throw new ArgumentInvalidException($"'{nameof(Input.PropItems)}' cannot be null.");

                if (Input.PropItems.Count() == 0)
                    throw new ArgumentInvalidException($"'{nameof(Input.PropItems)}' count must be greater than zero.");

                #endregion

                foreach (var item in Input.PropItems.Where(a => a.Value != null || a.Value != ""))
                {
                    if (await _ProductPropertisApplication.CheckExistByIdAsync(new InpCheckExistById { Id= item.Key }))
                        await _ProductPropertiesValuesRepository.AddAsync(new tblProductPropertiesValues()
                        {
                            Id = new Guid().SequentialGuid(),
                            ProductId = Guid.Parse(Input.ProductId),
                            ProductPropertiesId = Guid.Parse(item.Key),
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
                Input.CheckModelState();                
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
    }
}
