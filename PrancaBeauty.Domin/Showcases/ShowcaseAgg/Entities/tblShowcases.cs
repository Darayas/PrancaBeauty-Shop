using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.Showcases.ShowcaseAgg.Entities
{
    public class tblShowcases : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? CountryId { get; set; }
        public string Name { get; set; }
        public string BackgroundColorCode { get; set; }
        public string CssStyle { get; set; }
        public string CssClass { get; set; }
        public int Sort { get; set; }
        public bool IsFullWidth { get; set; }
        public bool IsEnable { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual tblCountries tblCountry { get; set; }
        public virtual tblUsers tblUser { get; set; }
        public virtual ICollection<tblShowcasesTranslates> tblShowcasesTranslates { get; set; }
        public virtual ICollection<tblShowcaseTabs> tblShowcaseTabs { get; set; }

    }
}
