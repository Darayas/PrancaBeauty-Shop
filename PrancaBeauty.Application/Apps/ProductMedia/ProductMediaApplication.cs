using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using PrancaBeauty.Application.Contracts.ProductMedia;
using PrancaBeauty.Application.Contracts.Results;
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
    }
}
