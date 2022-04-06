using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductAskLikes;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductReviewLikes;
using PrancaBeauty.Domin.Product.ProductAskLikesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductAskLikesAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductAskLikes
{
    public class ProductAskLikesApplication : IProductAskLikesApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILocalizer _Localizer;
        private readonly IProductAskLikesRepository _ProductAskLikesRepository;

        public ProductAskLikesApplication(IProductAskLikesRepository productAskLikesRepository, ILogger logger, IServiceProvider serviceProvider, ILocalizer localizer)
        {
            _ProductAskLikesRepository = productAskLikesRepository;
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _Localizer = localizer;
        }

        public async Task<(int CountLike, bool IsLike)> LikeAskAsync(InpLikeAsk Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qReviewLike = await _ProductAskLikesRepository.Get
                                                                  .Where(a => a.ProductAskId == Guid.Parse(Input.AskId))
                                                                  .Where(a => a.Type == ProductAskLikesEnum.Like)
                                                                  .Where(a => a.UserId == Guid.Parse(Input.UserId))
                                                                  .SingleOrDefaultAsync();
                bool _IsLike = false;
                if (qReviewLike != null)
                {
                    await _ProductAskLikesRepository.DeleteAsync(qReviewLike, default, true);
                    _IsLike = false;
                }
                else
                {
                    if (!await _ProductAskLikesRepository.Get.Where(a => a.ProductAskId == Guid.Parse(Input.AskId) && a.UserId == Guid.Parse(Input.UserId)).AnyAsync())
                    {
                        await _ProductAskLikesRepository.AddAsync(new tblProductAskLikes()
                        {
                            Id = new Guid().SequentialGuid(),
                            ProductAskId = Guid.Parse(Input.AskId),
                            UserId = Guid.Parse(Input.UserId),
                            Date = DateTime.Now,
                            Type = ProductAskLikesEnum.Like
                        }, default, true);
                        _IsLike = true;
                    }
                    else
                        _IsLike = false;
                }

                return (await _ProductAskLikesRepository.Get.Where(a => a.ProductAskId == Guid.Parse(Input.AskId))
                                                            .Where(a => a.Type == ProductAskLikesEnum.Like)
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

        public async Task<(int CountDisLike, bool IsDisLike)> DisLikeAskAsync(InpDisLikeAsk Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                // TODO کاربری که دیس لایک کرده باشد امکان لایک ندارد

                var qReviewDisLike = await _ProductAskLikesRepository.Get
                                                                   .Where(a => a.ProductAskId == Guid.Parse(Input.AskId))
                                                                   .Where(a => a.Type == ProductAskLikesEnum.Dislike)
                                                                   .Where(a => a.UserId == Guid.Parse(Input.UserId))
                                                                   .SingleOrDefaultAsync();
                bool _IsDisLike = false;
                if (qReviewDisLike != null)
                {
                    await _ProductAskLikesRepository.DeleteAsync(qReviewDisLike, default, true);
                    _IsDisLike = false;
                }
                else
                {
                    if (!await _ProductAskLikesRepository.Get.Where(a => a.ProductAskId == Guid.Parse(Input.AskId) && a.UserId == Guid.Parse(Input.UserId)).AnyAsync())
                    {
                        await _ProductAskLikesRepository.AddAsync(new tblProductAskLikes()
                        {
                            Id = new Guid().SequentialGuid(),
                            ProductAskId = Guid.Parse(Input.AskId),
                            UserId = Guid.Parse(Input.UserId),
                            Date = DateTime.Now,
                            Type = ProductAskLikesEnum.Dislike
                        }, default, true);
                        _IsDisLike = true;
                    }
                    else
                        _IsDisLike = false;
                }

                return (await _ProductAskLikesRepository.Get.Where(a => a.ProductAskId == Guid.Parse(Input.AskId))
                                                            .Where(a => a.Type == ProductAskLikesEnum.Dislike)
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
