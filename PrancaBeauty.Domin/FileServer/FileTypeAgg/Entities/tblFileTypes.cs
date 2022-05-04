using Framework.Domain.Contracts;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.FileServer.FileTypeAgg.Entities
{
    public class tblFileTypes : IEntity
    {
        public Guid Id { get; set; }
        public string IconUrl { get; set; }
        public string MimeType { get; set; }
        public string Extentions { get; set; }

        public virtual ICollection<tblFiles> tblFiles { get; set; }
    }
}
