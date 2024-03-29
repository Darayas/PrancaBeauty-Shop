﻿using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Entities;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Entities
{
    public class tblShowcaseTabs : IEntity
    {
        public Guid Id { get; set; }
        public Guid ShowcaseId { get; set; }
        public string Name { get; set; }
        public string BackgroundColorCode { get; set; }
        public int Sort { get; set; }
        public bool IsEnable { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual tblShowcases tblShowcases { get; set; }
        public virtual ICollection<tblShowcaseTabTranslates> tblShowcaseTabTranslates { get; set; }
        public virtual ICollection<tblShowcaseTabSections> tblShowcaseTabSections { get; set; }
    }
}
