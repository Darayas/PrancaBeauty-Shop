using Framework.Domain;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.StettingsAgg.Entities
{
    public class tblSettings : IEntity
    {
        public Guid Id { get; set; }
        public Guid LangId { get; set; }
        public string SiteTitle { get; set; }
        public string SiteUrl { get; set; }
        public string SiteDescription { get; set; }
        public string SiteEmail { get; set; }
        public string SitePhoneNumber { get; set; }
        public bool IsInManufacture { get; set; }


        public virtual tblLanguages tblLanguages { get; set; }
    }
}
