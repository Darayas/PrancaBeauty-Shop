using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Product.ProductGroupPercentAgg.Entities;
using PrancaBeauty.Domin.Product.ProductTopicAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.Product.ProductGroupAgg.Entities
{
    public class tblProductGroups : IEntity
    {
        public Guid Id { get; set; }
        public Guid TopicId { get; set; }
        public string Name { get; set; }

        public virtual tblProductTopic tblProductTopic { get; set; }
        public virtual ICollection<tblProductGroupTranslate> tblProductGroupTranslate { get; set; }
        public virtual ICollection<tblProductGroupPercents> tblProductGroupPercents { get; set; }
    }
}
