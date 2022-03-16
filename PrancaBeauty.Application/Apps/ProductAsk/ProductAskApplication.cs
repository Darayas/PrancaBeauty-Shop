using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ProductAsks;
using PrancaBeauty.Domin.Product.ProductAskAgg.Contarcts;
using PrancaBeauty.Domin.Product.ProductAskLikesAgg.Entities;
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
                                    .OrderByDescending(a => a.Date)
                                    .Select(ask => new OutGetListAsks
                                    {
                                        Id = ask.Id.ToString(),
                                        Text = ask.Text,
                                        LstAnswer = ask.tblProductAsk_Childs.Select(answer => new OutGetListAsks_Answer
                                        {
                                            Id = answer.Id.ToString(),
                                            FullName = answer.tblUsers.FirstName + " " + answer.tblUsers.LastName,
                                            Text = answer.Text,
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
    }
}
