using PrancaBeauty.Domin.KeywordAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Keywords
{
    public class KeywordApplication : IKeywordApplication
    {
        private readonly IKeywordRepository _KeywordRepository;
        public KeywordApplication(IKeywordRepository keywordRepository)
        {
            _KeywordRepository = keywordRepository;
        }

    }
}
