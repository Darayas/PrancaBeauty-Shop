using Framework.Domain;
using PrancaBeauty.Domin.Showcases.SectionFreeItemAgg.Entities;
using PrancaBeauty.Domin.Showcases.SectionItems.Entitiy;
using PrancaBeauty.Domin.Showcases.SectionProductAgg.Entities;
using PrancaBeauty.Domin.Showcases.SectionProductCategoryAgg.Entities;
using PrancaBeauty.Domin.Showcases.SectionProductKeywordAgg.Entities;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Entities
{
    public class tblShowcaseTabSections : IEntity
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public Guid ShowcaseTabId { get; set; }
        public string Name { get; set; }
        public int XlSize { get; set; } // Extra Larg
        public int LgSize { get; set; } // Larg 
        public int MdSize { get; set; } // Medium
        public int SmSize { get; set; } // Smal
        public int XsSize { get; set; } // Extra Small
        public bool IsSlider { get; set; }
        public int CountInSection { get; set; }

        public virtual tblShowcaseTabs tblShowcaseTabs { get; set; }
        public virtual tblShowcaseTabSections tblShowcaseTabSectionsParent { get; set; }
        public virtual ICollection<tblShowcaseTabSections> tblShowcaseTabSectionsChild { get; set; }
        public virtual ICollection<tblShowcaseTabSectionItems> tblShowcaseTabSectionItems { get; set; }
    }
}
