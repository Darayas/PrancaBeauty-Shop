using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ProductProperties;
using PrancaBeauty.Domin.Product.ProductPropertisAgg.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductPropertis
{
    public class ProductPropertisApplication : IProductPropertisApplication
    {
        private readonly ILogger _Logger;
        private readonly IProductPropertisRepository _ProductPropertisRepository;

        public ProductPropertisApplication(IProductPropertisRepository productPropertisRepository, ILogger logger)
        {
            _ProductPropertisRepository = productPropertisRepository;
            _Logger = logger;
        }

        public async Task<List<OutGetForManageProduct>> GetForManageProductAsync(InpGetForManageProduct Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState();
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
            Input.CheckModelState();
            #endregion

            return await _ProductPropertisRepository.Get.AnyAsync(a => a.Id == Guid.Parse(Input.Id));
        }
    }
}
