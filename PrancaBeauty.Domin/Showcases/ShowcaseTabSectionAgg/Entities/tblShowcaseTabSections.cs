using Framework.Domain;
using PrancaBeauty.Domin.Showcases.SectionCategories.Entities;
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

        public virtual tblShowcaseTabs tblShowcaseTabs { get; set; }
        public virtual tblShowcaseTabSections tblShowcaseTabSectionsParent { get; set; }
        public virtual ICollection<tblShowcaseTabSections> tblShowcaseTabSectionsChild { get; set; }
        public virtual ICollection<tblSectionCategories> tblSectionCategories { get; set; }
    }
}
