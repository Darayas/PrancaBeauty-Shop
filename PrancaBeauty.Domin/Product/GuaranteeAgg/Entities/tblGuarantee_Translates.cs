using Framework.Domain;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.GuaranteeAgg.Entities
{
    public class tblGuarantee_Translates : IEntity
    {
        public Guid Id { get; set; }
        public Guid GuaranteeId { get; set; }
        public Guid LangId { get; set; }
        public string Title { get; set; }

        public virtual tblGuarantee tblGuarantee { get; set; }
        public virtual tblLanguages tblLanguages { get; set; }
    }
}
