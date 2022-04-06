using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductMedia;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Domin.Product.ProductMediaAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductMediaAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductMedia
{
    public class ProductMediaApplication : IProductMediaApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductMediaRepository _ProductMediaRepository;

        public ProductMediaApplication(ILogger logger, IProductMediaRepository productMediaRepository, IServiceProvider serviceProvider)
        {
            _Logger = logger;
            _ProductMediaRepository = productMediaRepository;
            _ServiceProvider = serviceProvider;
        }

        public async Task<OperationResult> AddMediasToProductAsync(InpAddMediasToProduct Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                int i = 0;
                foreach (var item in Input.MediaIds.Split(","))
                {
                    if (string.IsNullOrEmpty(item))
                        continue;

                    var tMedia = new tblProductMedia()
                    {
                        Id = new Guid().SequentialGuid(),
                        ProductId = Guid.Parse(Input.ProductId),
                        FileId = Guid.Parse(item),
                        Sort = i
                    };

                    i++;
                    await _ProductMediaRepository.AddAsync(tMedia, default, false);
                }

                await _ProductMediaRepository.SaveChangeAsync();
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

        public async Task<OperationResult> RemoveAllMediaFromProductAsync(InpRemoveAllMediaFromProduct Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductMediaRepository.Get.Where(a => a.ProductId == Guid.Parse(Input.ProductId)).ToListAsync();

                await _ProductMediaRepository.DeleteRangeAsync(qData, default, true);

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

        public async Task<OperationResult> EditProductMediaAsync(InpEditProductMedia Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region حذف رسانه های قبلی
                {
                    var _Result = await RemoveAllMediaFromProductAsync(new InpRemoveAllMediaFromProduct
                    {
                        ProductId = Input.ProductId
                    });

                    if (_Result.IsSucceeded == false)
                        return new OperationResult().Failed(_Result.Message);
                }
                #endregion

                #region افزودن رسانه های جدید
                {
                    var _Result = await AddMediasToProductAsync(new InpAddMediasToProduct
                    {
                        ProductId = Input.ProductId,
                        MediaIds = Input.MediaIds
                    });

                    if (_Result.IsSucceeded == false)
                        return new OperationResult().Failed(_Result.Message);
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
