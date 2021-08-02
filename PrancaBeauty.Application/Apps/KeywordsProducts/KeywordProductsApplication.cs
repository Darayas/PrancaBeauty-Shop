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
        private readonly IKeywords_ProductsRepository _IKeywords_ProductsRepository;
        public KeywordProductsApplication(IKeywords_ProductsRepository iKeywords_ProductsRepository)
        {
            _IKeywords_ProductsRepository = iKeywords_ProductsRepository;
        }
    }
}
