using Framework.Domain;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Entities;
using PrancaBeauty.Domin.Keywords.KeywordAgg.Entities;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Showcases.SectionKeywords.Entities
{
    public class tblSectionKeywords : IEntity
    {
        public Guid Id { get; set; }
        public Guid ShowcaseTabSectionId { get; set; }
        public Guid KeywordId { get; set; }
        public int Count { get; set; }
        public tblSectionKeywordsOrderByEnum OrderBy { get; set; }
        public int Sort { get; set; }

        public virtual tblShowcaseTabSections tblShowcaseTabSections { get; set; }
        public virtual tblKeywords tblKeywords { get; set; }
    }

    public enum tblSectionKeywordsOrderByEnum
    {
        Newest,
        Oldest,
        Popular
    }
}
