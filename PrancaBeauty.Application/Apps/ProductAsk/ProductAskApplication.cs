using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ProdcutReviews;
using PrancaBeauty.Application.Contracts.ProductAsks;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Product.ProductAskAgg.Contarcts;
using PrancaBeauty.Domin.Product.ProductAskAgg.Entities;
using PrancaBeauty.Domin.Product.ProductAskLikesAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductAsk
{
    public class ProductAskApplication : IProductAskApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductAskRepository _ProductAskRepository;

        public ProductAskApplication(IProductAskRepository productAskRepository, ILogger logger, IServiceProvider serviceProvider)
        {
            _ProductAskRepository = productAskRepository;
            _Logger = logger;
            _ServiceProvider = serviceProvider;
        }

        public async Task<(OutPagingData PagingData, List<OutGetListAsks> LstAsks)> GetListAsksAsync(InpGetListAsks Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                // TODO: Check IsConfirm

                var qData = _ProductAskRepository.Get
                                    .Where(a => a.ProductId == Input.ProductId.ToGuid())
                                    .Where(a => a.AskId == null)
                                    .Where(a => Input.HasFullControl ? true : Input.UserId != null ? ((a.tblProducts.AuthorUserId == Input.UserId.ToGuid()) ? true : a.IsConfirm == true) : a.IsConfirm == true)
                                    .OrderByDescending(a => a.Date)
                                    .Select(ask => new OutGetListAsks
                                    {
                                        Id = ask.Id.ToString(),
                                        Text = ask.Text,
                                        IsConfirm = ask.IsConfirm,
                                        LstAnswer = ask.tblProductAsk_Childs
                                                       .Where(a => Input.HasFullControl ? true : Input.UserId != null ? ((a.tblProducts.AuthorUserId == Input.UserId.ToGuid()) ? true : a.IsConfirm == true) : a.IsConfirm == true)
                                                       .OrderByDescending(a => a.Date)
                                                       .Select(answer => new OutGetListAsks_Answer
                                                       {
                                                           Id = answer.Id.ToString(),
                                                           FullName = answer.tblUsers.FirstName + " " + answer.tblUsers.LastName,
                                                           Text = answer.Text,
                                                           IsConfirm = answer.IsConfirm,
                                                           IsLike = Input.UserId != null ? answer.tblProductAskLikes.Where(a => a.Type == ProductAskLikesEnum.Like).Any(a => a.UserId == Guid.Parse(Input.UserId)) : false,
                                                           IsDisLike = Input.UserId != null ? answer.tblProductAskLikes.Where(a => a.Type == ProductAskLikesEnum.Dislike).Any(a => a.UserId == Guid.Parse(Input.UserId)) : false,
                                                           CountLikes = answer.tblProductAskLikes.Count(c => c.Type == ProductAskLikesEnum.Like),
                                                           CountDisLike = answer.tblProductAskLikes.Count(c => c.Type == ProductAskLikesEnum.Dislike)
                                                       }).ToList()
                                    });

                var _PagingData = PagingData.Calc(await qData.CountAsync(), Input.Page, Input.Take);
                return (_PagingData, await qData.Skip((int)_PagingData.Skip).Take(_PagingData.Take).ToListAsync());
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

        public async Task<OperationResult> ChanageStatusAskAsync(InpChanageStatusAsk Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductAskRepository.Get
                                                .Where(a => a.Id == Input.AskId.ToGuid())
                                                .Where(a => Input.AuthorUserId != null ? a.tblProducts.AuthorUserId == Input.AuthorUserId.ToGuid() : true)
                                                .SingleOrDefaultAsync();

                if (qData == null)
                    return new OperationResult().Failed("ReviewNotFound");

                if (qData.IsConfirm)
                {
                    qData.IsConfirm = false;
                }
                else
                {
                    qData.IsConfirm = true;
                }

                await _ProductAskRepository.UpdateAsync(qData, default);

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

        public async Task<OperationResult> AddNewAskAsync(InpAddNewAsk Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var tProductAsk = new tblProductAsk
                {
                    Id = new Guid().SequentialGuid(),
                    ProductId = Input.ProductId.ToGuid(),
                    UserId = Input.UserId.ToGuid(),
                    AskId = Input.AskId == null ? null : Input.AskId.ToGuid(),
                    Date = DateTime.Now,
                    IsConfirm = false,
                    Text = Input.Text.RemoveAllHtmlTags()
                };

                await _ProductAskRepository.AddAsync(tProductAsk, default, true);

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

        public async Task<OperationResult> RemoveAskAsync(InpRemoveAsk Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductAskRepository.Get
                                                .Where(a => a.Id == Input.AskId.ToGuid())
                                                .Where(a => Input.UserId != null ? a.tblProducts.AuthorUserId == Input.UserId.ToGuid() : true)
                                                .Include(a => a.tblProductAsk_Childs)
                                                .SingleOrDefaultAsync();

                if (qData == null)
                    return new OperationResult().Failed("IdNotFound");

                #region Remove Child if exists
                {
                    //if (qData.tblProductAsk_Childs.Count() > 0)
                    //{
                    foreach (var item in qData.tblProductAsk_Childs)
                    {
                        await _ProductAskRepository.DeleteAsync(item, default, false);
                    }

                    await _ProductAskRepository.SaveChangeAsync();
                    //}
                }
                #endregion

                await _ProductAskRepository.DeleteAsync(qData, default, true);

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
