using Framework.Exceptions;
using Framework.Infrastructure;
using PrancaBeauty.Application.Apps.Keywords;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Keywords.Keywords_Products.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.KeywordsProducts
{
    public class KeywordProductsApplication : IKeywordProductsApplication
    {
        private readonly ILogger _Logger;
        private readonly IKeywords_ProductsRepository _IKeywords_ProductsRepository;
        private readonly IKeywordApplication _KeywordApplication;
        public KeywordProductsApplication(IKeywords_ProductsRepository iKeywords_ProductsRepository, ILogger logger, IKeywordApplication keywordApplication)
        {
            _IKeywords_ProductsRepository = iKeywords_ProductsRepository;
            _Logger = logger;
            _KeywordApplication = keywordApplication;
        }

        public async Task<OperationResult> AddKeywordsToProductAsync(string ProductId, List<(string, string)> LstKeyword)
        {
            try
            {
                #region Validations
                if (string.IsNullOrWhiteSpace(ProductId))
                    throw new ArgumentInvalidException($"'{nameof(ProductId)}' cannot be null or whitespace.");

                if (LstKeyword is null)
                    throw new ArgumentInvalidException($"'{nameof(LstKeyword)}' cannot be null.");
                #endregion

                foreach (var item in LstKeyword.Where(a => string.IsNullOrWhiteSpace(a.Item1) && string.IsNullOrEmpty(a.Item1))
                                               .Where(a => double.Parse(a.Item2) > 0))
                {
                    string KeywordId = null;
                    #region برسی موجود بودن کلمه کلیدی یا افزودن آن
                    {
                        if (await _KeywordApplication.CheckExistByTitleAsync(item.Item1))
                            KeywordId = await _KeywordApplication.GetIdByTitleAsync(item.Item2);
                        else
                        {

                        }
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }
    }
}
