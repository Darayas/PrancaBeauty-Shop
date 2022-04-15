using Framework.Domain;
using PrancaBeauty.Domin.Keywords.KeywordAgg.Entities;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Entities;
using System;

namespace PrancaBeauty.Domin.Showcases.SectionProductKeywordAgg.Entities
{
    public class tblSectionProductKeyword : IEntity
    {
        public Guid Id { get; set; }
        public Guid ShowcaseTabSectionId { get; set; }
        public Guid KeywordId { get; set; }
        public int CountFetch { get; set; }
        public tblSectionProductKeywordOrderByEnum OrderBy { get; set; }

        public virtual tblShowcaseTabSections tblShowcaseTabSections { get; set; }
        public virtual tblKeywords tblKeywords { get; set; }
    }

    public enum tblSectionProductKeywordOrderByEnum
    {
        Newest,
        Oldest,
        Popular
    }
}
