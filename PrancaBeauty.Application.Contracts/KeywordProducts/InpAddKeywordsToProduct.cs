using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.KeywordProducts
{
    public class InpAddKeywordsToProduct
    {
        public string ProductId { get; set; }
        public List<InpAddKeywordsToProduct_LstKeywords> LstKeywords { get; set; }
    }

    public class InpAddKeywordsToProduct_LstKeywords
    {
        public string Title { get; set; }
        public string Similarity { get; set; }
    }
}
