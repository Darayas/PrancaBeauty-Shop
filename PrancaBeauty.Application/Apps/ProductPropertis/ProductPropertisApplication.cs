using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ProductProperties;
using PrancaBeauty.Domin.Product.ProductPropertisAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductPropertis
{
    public class ProductPropertisApplication : IProductPropertisApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductPropertisRepository _ProductPropertisRepository;

        public ProductPropertisApplication(IProductPropertisRepository productPropertisRepository, ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _ProductPropertisRepository = productPropertisRepository;
            _Logger = logger;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<List<OutGetForManageProduct>> GetForManageProductAsync(InpGetForManageProduct Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductPropertisRepository.Get
                                                            .Where(a => a.TopicId == Guid.Parse(Input.TopicId))
                                                            .Select(a => new OutGetForManageProduct
                                                            {
                                                                PropertyId = a.Id.ToString(),
                                                                PropertyTitle = a.tblProductPropertis_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single(),
                                                                PropertyValue = ""
                                                            })
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

        public async Task<bool> CheckExistByIdAsync(InpCheckExistById Input)
        {
            #region Validation
            Input.CheckModelState(_ServiceProvider);
            #endregion

            return await _ProductPropertisRepository.Get.AnyAsync(a => a.Id == Guid.Parse(Input.Id));
        }
    }
}
