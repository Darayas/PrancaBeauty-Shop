using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrancaBeauty.Application.Contracts.ProductReviewLikes;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductReviewsLike
{
    public class ProductReviewsLikeApplication : IProductReviewsLikeApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductReviewsLikeRepository _ProductReviewsLikeRepository;

        public ProductReviewsLikeApplication(IProductReviewsLikeRepository productReviewsLikeRepository, ILogger logger, IServiceProvider serviceProvider)
        {
            _ProductReviewsLikeRepository = productReviewsLikeRepository;
            _Logger = logger;
            _ServiceProvider = serviceProvider;
        }

        public async Task<int> LikeReviewAsync(InpLikeReview Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qReviewLike = await _ProductReviewsLikeRepository.Get
                                                                   .Where(a => a.Id == Guid.Parse(Input.ReviewId))
                                                                   .Where(a => a.Type == ProductReviewsLikesEnum.Like)
                                                                   .Where(a => a.UserId == Guid.Parse(Input.UserId))
                                                                   .SingleOrDefaultAsync();

                if (qReviewLike != null)
                {
                    await _ProductReviewsLikeRepository.DeleteAsync(qReviewLike, default, true);

                }
                else
                {
                    await _ProductReviewsLikeRepository.AddAsync(new tblProductReviewsLikes()
                    {
                        Id = new Guid().SequentialGuid(),
                        ProductReviewId = Guid.Parse(Input.ReviewId),
                        UserId = Guid.Parse(Input.UserId),
                        Date = DateTime.Now,
                        Type = ProductReviewsLikesEnum.Like
                    }, default, true);
                }

                return await _ProductReviewsLikeRepository.Get.Where(a => a.Id == Guid.Parse(Input.ReviewId))
                                                              .Where(a => a.Type == ProductReviewsLikesEnum.Like)
                                                              .CountAsync();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return -1;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return -1;
            }
        }
    }
}
