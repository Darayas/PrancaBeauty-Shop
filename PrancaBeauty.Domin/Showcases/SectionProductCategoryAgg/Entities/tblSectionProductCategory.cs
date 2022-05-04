using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Entities;
using PrancaBeauty.Domin.Showcases.SectionItems.Entitiy;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Entities;
using System;

namespace PrancaBeauty.Domin.Showcases.SectionProductCategoryAgg.Entities
{
    public class tblSectionProductCategory : IEntity
    {
        public Guid Id { get; set; }
        public Guid TabSectionItemId { get; set; }
        public Guid CategoryId { get; set; }
        public int CountFetch { get; set; }
        public tblSectionProductCategoryOrderByEnum OrderBy { get; set; }


        public virtual tblShowcaseTabSectionItems tblShowcaseTabSectionItems { get; set; }
        public virtual tblCategoris tblCategory { get; set; }
    }

    public enum tblSectionProductCategoryOrderByEnum
    {
        Newest,
        Oldest,
        Popular
    }
}
