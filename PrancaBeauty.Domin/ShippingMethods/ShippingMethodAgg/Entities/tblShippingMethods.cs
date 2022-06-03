using Framework.Domain.Contracts;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodRestrictAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.ShippingMethods.ShippingMethodAgg.Entities
{
    public class tblShippingMethods : IEntity
    {
        public Guid Id { get; set; }
        public Guid LogoId { get; set; }
        public string Name { get; set; }

        public virtual tblFiles tblFiles { get; set; }
        public virtual ICollection<tblShippingMethodTranslate> tblShippingMethodTranslate { get; set; }
        public virtual ICollection<tblShippingMethodRestricts> tblShippingMethodRestricts { get; set; }
    }
}
