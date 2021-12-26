using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.PostingRestrictions;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Product.PostingRestrictionsAgg.Contracts;
using PrancaBeauty.Domin.Product.PostingRestrictionsAgg.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.PostingRestrictions
{
    public class PostingRestrictionsApplication : IPostingRestrictionsApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IPostingRestrictionsRepository _PostingRestrictionsRepository;

        public PostingRestrictionsApplication(IPostingRestrictionsRepository postingRestrictionsRepository, ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _PostingRestrictionsRepository = postingRestrictionsRepository;
            _Logger = logger;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<OperationResult> AddPostingRestrictionsToProductAsync(InpAddPostingRestrictionsToProduct Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                foreach (var item in Input.PostingRestrictions)
                {
                    var tPostingRestrictions = new tblPostingRestrictions()
                    {
                        Id = new Guid().SequentialGuid(),
                        ProductId = Guid.Parse(Input.ProductId),
                        CountryId = Guid.Parse(item.CountryId),
                        Posting = item.Posting
                    };

                    await _PostingRestrictionsRepository.AddAsync(tPostingRestrictions, default, false);
                }

                await _PostingRestrictionsRepository.SaveChangeAsync();

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

        public async Task<OperationResult> RemoveAllPostingRestrictionsFromProductAsync(InpRemoveAllPostingRestrictionsFromProduct Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _PostingRestrictionsRepository.Get.Where(a => a.ProductId == Guid.Parse(Input.ProductId)).ToListAsync();

                await _PostingRestrictionsRepository.DeleteRangeAsync(qData, default, true);

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

        public async Task<List<OutGetAllPostingRestrictionsByProductId>> GetAllPostingRestrictionsByProductIdAsync(InpGetAllPostingRestrictionsByProductId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _PostingRestrictionsRepository.Get
                                                                .Where(a => a.ProductId == Guid.Parse(Input.ProductId))
                                                                .Select(a => new OutGetAllPostingRestrictionsByProductId
                                                                {
                                                                    Id = a.Id.ToString(),
                                                                    CountryId = a.CountryId.ToString(),
                                                                    Posting = a.Posting
                                                                })
                                                                .ToListAsync();

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

        public async Task<OperationResult> EditPostingRestrictionsAsync(InpEditPostingRestrictions Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region حذف محدودیت های قبلی
                {
                    var _Result = await RemoveAllPostingRestrictionsFromProductAsync(new InpRemoveAllPostingRestrictionsFromProduct
                    {
                        ProductId = Input.ProductId
                    });
                    if (_Result.IsSucceeded == false)
                        return new OperationResult().Failed(_Result.Message);
                }
                #endregion

                #region افزودن محدودیت های جدید
                {
                    var _Result = await AddPostingRestrictionsToProductAsync(new InpAddPostingRestrictionsToProduct
                    {
                        ProductId = Input.ProductId,
                        PostingRestrictions = Input.PostingRestrictions.Select(a => new InpAddPostingRestrictionsToProduct_Restrictions
                        {
                            CountryId = a.CountryId,
                            Posting = a.Posting
                        }).ToList()
                    });
                    if (_Result.IsSucceeded == false)
                        return new OperationResult().Failed(_Result.Message);
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
