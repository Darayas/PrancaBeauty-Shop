using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;

namespace PrancaBeauty.Domin.ShippingMethods.ShippingMethodAgg.Entities
{
    public class tblShippingMethodTranslate : IEntity
    {
        public Guid Id { get; set; }
        public Guid ShippingMethodId { get; set; }
        public Guid LangId { get; set; }
        public string Title { get; set; }

        public virtual tblShippingMethods tblShippingMethods { get; set; }
        public virtual tblLanguages tblLanguages { get; set; }
    }
}
