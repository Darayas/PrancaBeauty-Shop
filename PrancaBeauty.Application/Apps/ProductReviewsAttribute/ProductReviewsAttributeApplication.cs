using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductReviewsAttributes;
using PrancaBeauty.Domin.Product.ProductReviewsAttributeAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductReviewsAttribute
{
    public class ProductReviewsAttributeApplication : IProductReviewsAttributeApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductReviewsAttributeRepository _ProductReviewsAttributeRepository;

        public ProductReviewsAttributeApplication(IProductReviewsAttributeRepository productReviewsAttributeRepository, ILogger logger, IServiceProvider serviceProvider)
        {
            _ProductReviewsAttributeRepository = productReviewsAttributeRepository;
            _Logger = logger;
            _ServiceProvider = serviceProvider;
        }

        public async Task<List<OutGetAttributesByTopicId>> GetAttributesByTopicIdAsync(InpGetAttributesByTopicId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductReviewsAttributeRepository.Get
                                                                    .Where(a => a.TopicId == Guid.Parse(Input.TopicId))
                                                                    .Select(a => new OutGetAttributesByTopicId
                                                                    {
                                                                        Id = a.Id.ToString(),
                                                                        Name = a.Name,
                                                                        Title = a.tblProductReviewsAttribute_Translate.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single()
                                                                    })
                                                                    .ToListAsync();

                if (qData == null)
                    return null;

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;
            }
        }
    }
}
