using Framework.Application.Consts;
using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ProdcutReviews;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Contracts;
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
        public ProductReviewsApplication(IProductReviewsRepository productReviewsRepository, ILogger logger, ILocalizer localizer)
        {
            _ProductReviewsRepository = productReviewsRepository;
            _Logger = logger;
            _Localizer = localizer;
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
                                                         SellerId = a.tblProductSellers.SellerId.ToString(),
                                                         SellerFullName = a.tblProductSellers.tblSellers.tblSeller_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single(),
                                                         SellerImgUrl = a.tblProductSellers.tblSellers.tblSeller_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.LogoId != null ? (b.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                                                                                                                                          + b.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                                                                                                                                          + b.tblFiles.tblFilePaths.Path
                                                                                                                                                                                          + b.tblFiles.FileName) : PublicConst.DefaultSellerLogoImg).Single(),
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
    }
}
