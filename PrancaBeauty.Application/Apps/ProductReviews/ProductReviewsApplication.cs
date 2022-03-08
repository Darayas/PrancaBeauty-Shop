using Framework.Application.Consts;
using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PrancaBeauty.Application.Apps.ProductReviewsAttributeValues;
using PrancaBeauty.Application.Apps.ProductReviewsMedia;
using PrancaBeauty.Application.Contracts.ProdcutReviews;
using PrancaBeauty.Application.Contracts.ProdcutReviewsMedia;
using PrancaBeauty.Application.Contracts.ProductReviewsAttributeValues;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductReviews
{
    public class ProductReviewsApplication : IProductReviewsApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILocalizer _Localizer;
        private readonly IProductReviewsRepository _ProductReviewsRepository;
        private readonly IProductReviewsMediaApplication _ProductReviewsMediaApplication;
        private readonly IProductReviewsAttributeValuesApplication _ProductReviewsAttributeValuesApplication;
        public ProductReviewsApplication(IProductReviewsRepository productReviewsRepository, ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, IProductReviewsMediaApplication productReviewsMediaApplication, IProductReviewsAttributeValuesApplication productReviewsAttributeValuesApplication)
        {
            _ProductReviewsRepository = productReviewsRepository;
            _Logger = logger;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
            _ProductReviewsMediaApplication = productReviewsMediaApplication;
            _ProductReviewsAttributeValuesApplication = productReviewsAttributeValuesApplication;
        }

        public async Task<(OutPagingData PageingData, List<OutGetReviewsForProductDetails> LstRevivews)> GetReviewsForProductDetailsAsync(InpGetReviewsForProductDetails Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get data
                var qData = _ProductReviewsRepository.Get
                                                     .Where(a => a.ProductId == Guid.Parse(Input.ProductId))
                                                     .Select(a => new OutGetReviewsForProductDetails
                                                     {
                                                         Id = a.Id.ToString(),
                                                         UserProfileImage = a.tblUsers.ProfileImgId != null ? a.tblUsers.tblProfileImage.tblFilePaths.tblFileServer.HttpDomin
                                                                                                                + a.tblUsers.tblProfileImage.tblFilePaths.tblFileServer.HttpPath
                                                                                                                + a.tblUsers.tblProfileImage.tblFilePaths.Path
                                                                                                                + a.tblUsers.tblProfileImage.FileName : PublicConst.DefaultUserProfileImg,
                                                         UserFullName = a.tblUsers.FirstName + " " + a.tblUsers.LastName,
                                                         IsBuyer = false, // TODO: همزمان با قسمت فاکتور ها تکمیل شود
                                                                          //SellerId = a.tblProductSellers.SellerId.ToString(),
                                                                          //SellerFullName = a.tblProductSellers.tblSellers.tblSeller_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single(),
                                                                          //SellerImgUrl = a.tblProductSellers.tblSellers.tblSeller_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.LogoId != null ? (b.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                          //+ b.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                          // + b.tblFiles.tblFilePaths.Path
                                                                          //+ b.tblFiles.FileName) : PublicConst.DefaultSellerLogoImg).Single(),
                                                         Date = a.Date,
                                                         Advantages = a.Advantages,
                                                         DisAdvantages = a.DisAdvantages,
                                                         IsConfirm = a.IsConfirm,
                                                         IsRead = a.IsRead,
                                                         Text = a.Text,
                                                         CountStar = a.CountStar,
                                                         CountLikes = a.tblProductReviewsLikes.Where(a => a.Type == ProductReviewsLikesEnum.Like).Count(),
                                                         CountDislike = a.tblProductReviewsLikes.Where(a => a.Type == ProductReviewsLikesEnum.Dislike).Count(),
                                                         LstAttributes = a.tblProductReviewsAttributeValues.Select(b => new OutGetReviewsForProductDetailsAttributes
                                                         {
                                                             Id = b.Id.ToString(),
                                                             Value = b.Value,
                                                             Title = b.tblProductReviewsAttribute.tblProductReviewsAttribute_Translate.Where(c => c.LangId == Guid.Parse(Input.LangId)).Select(c => c.Title).Single()
                                                         }).ToList(),
                                                         LstMedia = a.tblProductReviewsMedia.OrderBy(b => b.Sort).Select(b => new OutGetReviewsForProductDetailsMedia
                                                         {
                                                             Id = b.Id.ToString(),
                                                             FileUrl = b.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                        + b.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                        + b.tblFiles.tblFilePaths.Path
                                                                        + b.tblFiles.FileName
                                                         }).ToList()
                                                     })
                                                     .OrderByDescending(a => a.Date);
                #endregion

                #region Paging data
                var _PagingData = PagingData.Calc(await qData.LongCountAsync(), Input.Page, Input.Take);
                #endregion

                return (_PagingData, await qData.Skip((int)_PagingData.Skip).Take(Input.Take).ToListAsync());
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

        public async Task<OperationResult> RemoveProductReviewAsync(InpRemoveProductReview Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductReviewsRepository.Get.Where(a => a.Id == Guid.Parse(Input.ProductReviewId)).SingleOrDefaultAsync();
                if (qData == null)
                    return new OperationResult().Failed("IdIsInvalid");

                await _ProductReviewsRepository.DeleteAsync(qData, default, true);

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

        public async Task<OperationResult> AddReviewFromUserAsync(InpAddReviewFromUser Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Add review to product
                string _ProductReviewId = new Guid().SequentialGuid().ToString();
                {
                    var tProductReview = new tblProductReviews
                    {
                        Id = Guid.Parse(_ProductReviewId),
                        ProductId = Guid.Parse(Input.ProductId),
                        ProductSellerId = null,
                        AuthorUserId = Guid.Parse(Input.AuthorUserId),
                        Advantages = string.Join(',', JArray.Parse(Input.Advantages).Select(a => a.Value<string>("value"))),
                        DisAdvantages = string.Join(',', JArray.Parse(Input.DisAdvantages).Select(a => a.Value<string>("value"))),
                        CountStar = Input.CountStar,
                        Date = DateTime.Now,
                        IpAddress = Input.IpAddress,
                        IsConfirm = false,
                        IsRead = false,
                        Text = Input.Text.RemoveAllHtmlTags()
                    };

                    await _ProductReviewsRepository.AddAsync(tProductReview, default, true);
                }
                #endregion

                #region Add media to review
                {
                    var _Result = await _ProductReviewsMediaApplication.AddMediaToReviewAsync(new InpAddMediaToReview
                    {
                        ProductReviewId = _ProductReviewId,
                        MediaIds = Input.MediaIds
                    });
                    if (!_Result.IsSucceeded)
                    {
                        await RemoveProductReviewAsync(new InpRemoveProductReview { ProductReviewId = _ProductReviewId });
                    }
                }
                #endregion

                #region Add attribute to review
                {
                    var _Result = await _ProductReviewsAttributeValuesApplication.AddAttributesToReviewAsync(new InpAddAttributesToReview
                    {
                        ProductReviewId = _ProductReviewId,
                        Items = Input.LstAttribute.Select(a => new InpAddAttributesToReviewItem { AttributeId = a.AttributeId, Value = a.Value }).ToList()
                    });
                    if (!_Result.IsSucceeded)
                    {
                        await RemoveProductReviewAsync(new InpRemoveProductReview { ProductReviewId = _ProductReviewId });
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
