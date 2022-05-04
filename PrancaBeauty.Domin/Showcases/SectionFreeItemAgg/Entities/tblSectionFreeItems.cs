using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Showcases.SectionItems.Entitiy;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.Showcases.SectionFreeItemAgg.Entities
{
    public class tblSectionFreeItems : IEntity
    {
        public Guid Id { get; set; }
        public Guid TabSectionItemId { get; set; }
        public string Name { get; set; }

        public virtual tblShowcaseTabSectionItems tblShowcaseTabSectionItems { get; set; }
        public virtual ICollection<tblSectionFreeItemTranslate> tblSectionFreeItemTranslate { get; set; }

    }
}
