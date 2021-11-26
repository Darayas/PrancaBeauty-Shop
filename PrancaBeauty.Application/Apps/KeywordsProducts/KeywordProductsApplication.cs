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
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.KeywordsProducts
{
    public class KeywordProductsApplication : IKeywordProductsApplication
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IKeywords_ProductsRepository _IKeywords_ProductsRepository;
        private readonly IKeywordApplication _KeywordApplication;
        public KeywordProductsApplication(IKeywords_ProductsRepository iKeywords_ProductsRepository, ILogger logger, IKeywordApplication keywordApplication, IMapper mapper)
        {
            _IKeywords_ProductsRepository = iKeywords_ProductsRepository;
            _Logger = logger;
            _KeywordApplication = keywordApplication;
            _Mapper = mapper;
        }

        public async Task<OperationResult> AddKeywordsToProductAsync(InpAddKeywordsToProduct Input)
        {
            try
            {
                #region Validations
                if (Input is null)
                    throw new ArgumentInvalidException($"{nameof(Input)} cant be null.");

                List<ValidationResult> _ValidationResult = null;
                if (Validator.TryValidateObject(Input, new System.ComponentModel.DataAnnotations.ValidationContext(Input), _ValidationResult))
                    throw new ArgumentInvalidException(string.Join(",", _ValidationResult.Select(a => a.ErrorMessage)));
                #endregion

                foreach (var item in Input.LstKeywords.Where(a => string.IsNullOrWhiteSpace(a.Title) && string.IsNullOrEmpty(a.Title))
                                               .Where(a => double.Parse(a.Similarity) > 0))
                {
                    string KeywordId = null;
                    #region برسی موجود بودن کلمه کلیدی یا افزودن آن
                    {
                        if (await _KeywordApplication.CheckExistByTitleAsync(new InpCheckExistByTitle { Title= item.Title }))
                            KeywordId = await _KeywordApplication.GetIdByTitleAsync(new InpGetIdByTitle { Title= item.Title });
                        else
                        {
                            var _Result = await _KeywordApplication.AddKeywordAsync(_Mapper.Map<InpAddKeyword>(item));
                            if (_Result.IsSucceeded)
                                KeywordId = await _KeywordApplication.GetIdByTitleAsync(new InpGetIdByTitle { Title= item.Title });
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
                        Similarity = double.Parse(item.Similarity)
                    };

                    await _IKeywords_ProductsRepository.AddAsync(tKeywordProducts, default, false);
                }

                await _IKeywords_ProductsRepository.SaveChangeAsync();

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
                Input.CheckModelState();
                #endregion

                var qData = await _IKeywords_ProductsRepository.Get.Where(a => a.ProductId == Guid.Parse(Input.ProductId)).ToListAsync();

                await _IKeywords_ProductsRepository.DeleteRangeAsync(qData, default, true);

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
