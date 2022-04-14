using Framework.Domain;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Entities
{
    public class tblShowcaseTabTranslates : IEntity
    {
        public Guid Id { get; set; }
        public Guid LangId { get; set; }
        public Guid ShowcaseTabId { get; set; }
        public string Title { get; set; }

        public virtual tblLanguages tblLanguages { get; set; }
        public virtual tblShowcaseTabs tblShowcaseTabs { get; set; }
    }
}
