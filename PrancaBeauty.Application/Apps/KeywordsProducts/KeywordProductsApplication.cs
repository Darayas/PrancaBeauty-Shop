using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.Keywords;
using PrancaBeauty.Application.Contracts.KeywordProducts;
using PrancaBeauty.Application.Contracts.Keywords;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Keywords.Keywords_Products.Contracts;
using PrancaBeauty.Domin.Keywords.Keywords_Products.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.KeywordsProducts
{
    public class KeywordProductsApplication : IKeywordProductsApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IKeywords_ProductsRepository _Keywords_ProductsRepository;
        private readonly IKeywordApplication _KeywordApplication;
        public KeywordProductsApplication(IKeywords_ProductsRepository Keywords_ProductsRepository, ILogger logger, IKeywordApplication keywordApplication, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _Keywords_ProductsRepository = Keywords_ProductsRepository;
            _Logger = logger;
            _KeywordApplication = keywordApplication;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<OperationResult> AddKeywordsToProductAsync(InpAddKeywordsToProduct Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                foreach (var item in Input.LstKeywords.Where(a => double.Parse(a.Similarity, new CultureInfo("en-US")) > 0))
                {
                    string KeywordId = null;
                    #region برسی موجود بودن کلمه کلیدی یا افزودن آن
                    {
                        if (await _KeywordApplication.CheckExistByTitleAsync(new InpCheckExistByTitle { Title = item.Title }))
                            KeywordId = await _KeywordApplication.GetIdByTitleAsync(new InpGetIdByTitle { Title = item.Title });
                        else
                        {
                            var _Result = await _KeywordApplication.AddKeywordAsync(new InpAddKeyword { Title = item.Title, Description = "" });
                            if (_Result.IsSucceeded)
                                KeywordId = await _KeywordApplication.GetIdByTitleAsync(new InpGetIdByTitle { Title = item.Title });
                            else
                                continue;
                        }
                    }
                    #endregion

                    var tKeywordProducts = new tblKeywords_Products()
                    {
                        Id = new Guid().SequentialGuid(),
                        KeywordId = Guid.Parse(KeywordId),
                        ProductId = Guid.Parse(Input.ProductId),
                        Similarity = double.Parse(item.Similarity, new CultureInfo("en-US"))
                    };

                    await _Keywords_ProductsRepository.AddAsync(tKeywordProducts, default, false);
                }

                await _Keywords_ProductsRepository.SaveChangeAsync();

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

        public async Task<OperationResult> RemoveAllProductKeywordsAsync(InpRemoveAllProductKeywords Input)
        {
            try
            {
                #region Validatons
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _Keywords_ProductsRepository.Get.Where(a => a.ProductId == Guid.Parse(Input.ProductId)).ToListAsync();

                await _Keywords_ProductsRepository.DeleteRangeAsync(qData, default, true);

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

        public async Task<List<OutGetKeywordByProductId>> GetKeywordByProductIdAsync(InpGetKeywordByProductId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _Keywords_ProductsRepository.Get
                                                             .Where(a => a.ProductId == Guid.Parse(Input.ProductId))
                                                             .Select(a => new OutGetKeywordByProductId
                                                             {
                                                                 Id = a.Id.ToString(),
                                                                 Title = a.tblKeywords.Title,
                                                                 Similarity = a.Similarity
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

        public async Task<OperationResult> EditProductKeywordsAsync(InpEditProductKeywords Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region حذف کلمات کلیدی موجود
                {
                    var _Result = await RemoveAllProductKeywordsAsync(new InpRemoveAllProductKeywords()
                    {
                        ProductId = Input.ProductId
                    });
                    if (_Result.IsSucceeded == false)
                        return new OperationResult().Failed(_Result.Message);
                }
                #endregion

                #region افزودن کلمات کلیدی جدید
                {
                    var _Result = await AddKeywordsToProductAsync(new InpAddKeywordsToProduct()
                    {
                        ProductId = Input.ProductId,
                        LstKeywords = Input.LstKeywords.Select(a => new InpAddKeywordsToProduct_LstKeywords
                        {
                            Title = a.Title,
                            Similarity = a.Similarity
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
