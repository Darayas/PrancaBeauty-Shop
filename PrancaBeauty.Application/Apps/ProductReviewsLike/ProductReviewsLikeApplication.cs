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

        public async Task<(int CountLike, bool IsLike)> LikeReviewAsync(InpLikeReview Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                // TODO کاربری که دیس لایک کرده باشد امکان لایک ندارد

                var qReviewLike = await _ProductReviewsLikeRepository.Get
                                                                   .Where(a => a.ProductReviewId == Guid.Parse(Input.ReviewId))
                                                                   .Where(a => a.Type == ProductReviewsLikesEnum.Like)
                                                                   .Where(a => a.UserId == Guid.Parse(Input.UserId))
                                                                   .SingleOrDefaultAsync();
                bool _IsLike = false;
                if (qReviewLike != null)
                {
                    await _ProductReviewsLikeRepository.DeleteAsync(qReviewLike, default, true);
                    _IsLike = false;
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
                    _IsLike = true;
                }

                return (await _ProductReviewsLikeRepository.Get.Where(a => a.ProductReviewId == Guid.Parse(Input.ReviewId))
                                                              .Where(a => a.Type == ProductReviewsLikesEnum.Like)
                                                              .CountAsync(), _IsLike);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return (-1, false);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return (-1, false);
            }
        }

        public async Task<(int CountDisLike, bool IsDisLike)> DisLikeReviewAsync(InpDisLikeReview Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                // TODO کاربری که دیس لایک کرده باشد امکان لایک ندارد

                var qReviewDisLike = await _ProductReviewsLikeRepository.Get
                                                                   .Where(a => a.ProductReviewId == Guid.Parse(Input.ReviewId))
                                                                   .Where(a => a.Type == ProductReviewsLikesEnum.Dislike)
                                                                   .Where(a => a.UserId == Guid.Parse(Input.UserId))
                                                                   .SingleOrDefaultAsync();
                bool _IsDisLike = false;
                if (qReviewDisLike != null)
                {
                    await _ProductReviewsLikeRepository.DeleteAsync(qReviewDisLike, default, true);
                    _IsDisLike = false;
                }
                else
                {
                    await _ProductReviewsLikeRepository.AddAsync(new tblProductReviewsLikes()
                    {
                        Id = new Guid().SequentialGuid(),
                        ProductReviewId = Guid.Parse(Input.ReviewId),
                        UserId = Guid.Parse(Input.UserId),
                        Date = DateTime.Now,
                        Type = ProductReviewsLikesEnum.Dislike
                    }, default, true);
                    _IsDisLike = true;
                }

                return (await _ProductReviewsLikeRepository.Get.Where(a => a.ProductReviewId == Guid.Parse(Input.ReviewId))
                                                              .Where(a => a.Type == ProductReviewsLikesEnum.Dislike)
                                                              .CountAsync(), _IsDisLike);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return (-1, false);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return (-1, false);
            }
        }
    }
}
