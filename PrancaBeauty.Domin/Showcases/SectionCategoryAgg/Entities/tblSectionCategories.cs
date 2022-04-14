using Framework.Domain;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Entities;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.Showcases.SectionCategoryAgg.Entities
{
    public class tblSectionCategories : IEntity
    {
        public Guid Id { get; set; }
        public Guid ShowcaseTabSectionId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }

        public virtual tblShowcaseTabSections tblShowcaseTabSections { get; set; }
        public virtual tblCategoris tblCategoris { get; set; }
        public virtual ICollection<tblSectionCategoryTranslate> tblSectionCategoryTranslate { get; set; }

    }
}
