using Framework.Domain;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductTopic
{
    public class tblProductTopic : IEntity
    {
        public Guid Id { get; set; }
        public Guid FileId { get; set; }
        public string Name { get; set; }

        public virtual tblFiles tblFiles { get; set; }
    }
}
