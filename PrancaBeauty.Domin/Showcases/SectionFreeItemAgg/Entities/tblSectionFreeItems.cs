using Framework.Domain;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Entities;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.Showcases.SectionFreeItemAgg.Entities
{
    public class tblSectionFreeItems : IEntity
    {
        public Guid Id { get; set; }
        public Guid ShowcaseTabSectionId { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }

        public virtual tblShowcaseTabSections tblShowcaseTabSections { get; set; }
        public virtual ICollection<tblSectionFreeItemTranslate> tblSectionFreeItemTranslate { get; set; }

    }
}
