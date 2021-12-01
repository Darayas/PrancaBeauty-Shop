using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.KeywordProducts
{
    public class InpAddKeywordsToProduct
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }
        public List<InpAddKeywordsToProduct_LstKeywords> LstKeywords { get; set; }
    }

    public class InpAddKeywordsToProduct_LstKeywords
    {
        [Display(Name = "KeywordTitle")]
        [RequiredString]
        [MaxLengthString(250)]
        public string Title { get; set; }

        [Display(Name = "KeywordSimilarityTitle")]
        [RequiredString]
        [MaxLengthString(10)]
        public string Similarity { get; set; }
    }
}
