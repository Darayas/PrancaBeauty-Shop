using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ProdcutReviewsMedia;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.FileServer.FileTypeAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsMediaAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsMediaAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductReviewsMedia
{
    public class ProductReviewsMediaApplication : IProductReviewsMediaApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductReviewsMediaRepository _ProductReviewsMediaRepository;

        public ProductReviewsMediaApplication(IProductReviewsMediaRepository productReviewsMediaRepository, ILogger logger, IServiceProvider serviceProvider)
        {
            _ProductReviewsMediaRepository = productReviewsMediaRepository;
            _Logger = logger;
            _ServiceProvider = serviceProvider;
        }

        public async Task<OperationResult> AddMediaToReviewAsync(InpAddMediaToReview Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                int Index = 0;
                foreach (var item in Input.MediaIds.Split(','))
                {
                    var tProductReviewMedia = new tblProductReviewsMedia
                    {
                        Id = new Guid().SequentialGuid(),
                        ProductReviewsId = Guid.Parse(Input.ProductReviewId),
                        FileId = Guid.Parse(item),
                        Sort = Index
                    };

                    await _ProductReviewsMediaRepository.AddAsync(tProductReviewMedia, default, false);
                    Index++;
                }

                await _ProductReviewsMediaRepository.SaveChangeAsync();

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

        public async Task<List<OutGetAllReviewMedia>> GetAllReviewMediaAsync(InpGetAllReviewMedia Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductReviewsMediaRepository.Get
                                                    .Where(a => a.tblProductReviews.ProductId == Guid.Parse(Input.ProductId))
                                                    .Select(a => new OutGetAllReviewMedia
                                                    {
                                                        Id = a.Id.ToString(),
                                                        CommentId = a.ProductReviewsId.ToString(),
                                                        MimeType = a.tblFiles.tblFileTypes.MimeType,
                                                        FileUrl = a.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                        + a.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                        + a.tblFiles.tblFilePaths.Path
                                                                        + a.tblFiles.FileName
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
    }
}
