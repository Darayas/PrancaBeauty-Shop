﻿using Framework.Domain;
using PrancaBeauty.Domin.Showcases.SectionFreeItemAgg.Entities;
using PrancaBeauty.Domin.Showcases.SectionProductAgg.Entities;
using PrancaBeauty.Domin.Showcases.SectionProductCategoryAgg.Entities;
using PrancaBeauty.Domin.Showcases.SectionProductKeywordAgg.Entities;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Entities;
using System;

namespace PrancaBeauty.Domin.Showcases.SectionItems.Entitiy
{
    public class tblShowcaseTabSectionItems : IEntity
    {
        public Guid Id { get; set; }
        public Guid TabSectionId { get; set; }
        public int Sort { get; set; }
        public tblShowcaseTabSectionItemsEnum SectionType { get; set; }


        public virtual tblShowcaseTabSections tblShowcaseTabSections { get; set; }
        public virtual tblSectionFreeItems tblSectionFreeItems { get; set; }
        public virtual tblSectionProducts tblSectionProducts { get; set; }
        public virtual tblSectionProductCategory tblSectionProductCategory { get; set; }
        public virtual tblSectionProductKeyword tblSectionProductKeyword { get; set; }
    }

    public enum tblShowcaseTabSectionItemsEnum
    {
        FreeItem = 0,
        Product = 1,
        Category = 2,
        Keyword = 3
    }
}
