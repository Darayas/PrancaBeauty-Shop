using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using PrancaBeauty.Application.Contracts.ProdcutReviews;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Contracts;
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

                var qData = _ProductReviewsRepository.Get
                                                     .Where(a => a.ProductId == Guid.Parse(Input.ProductId))
                                                     .Select(a => new OutGetReviewsForProductDetails
                                                     {
                                                         Id = a.Id.ToString(),
                                                         UserFullName = a.tblUsers.FirstName + " " + a.tblUsers.LastName,
                                                         SellerId = a.tblProductSellers.SellerId.ToString(),
                                                         SellerFullName = a.tblProductSellers.tblSellers.tblSeller_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single(),
                                                         SellerImgUrl = a.tblProductSellers.tblSellers.tblSeller_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                                                                                                                                         + b.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                                                                                                                                         + b.tblFiles.tblFilePaths.Path
                                                                                                                                                                                         + b.tblFiles.FileName).Single(),
                                                         Date=a.Date,
                                                         Advantages=a.Advantages,
                                                         DisAdvantages=a.DisAdvantages,
                                                         IsConfirm=a.IsConfirm,
                                                         IsRead=a.IsRead,
                                                         Text=a.Text,
                                                         
                                                     });
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
