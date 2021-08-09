using Framework.Domain;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductImagesAgg.Entities
{
    public class tblProductImages : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid FileId { get; set; }
        public int Sort { get; set; }

        public virtual tblProducts tblProducts { get; set; }
        public virtual tblFiles tblFiles { get; set; }
    }
}
