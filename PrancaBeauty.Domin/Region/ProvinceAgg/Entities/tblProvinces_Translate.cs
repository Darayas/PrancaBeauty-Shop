using Framework.Domain;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Region.ProvinceAgg.Entities
{
    public class tblProvinces_Translate : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProvinceId { get; set; }
        public Guid LangId { get; set; }
        public string Title { get; set; }

        public virtual tblProvinces tblProvince { get; set; }
        public virtual tblLanguages tblLanguage { get; set; }
    }
}
