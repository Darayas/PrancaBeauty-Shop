using Framework.Domain.Contracts;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.FileServer.ServerAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.FileServer.FilePathAgg.Entities
{
    public class tblFilePaths : IEntity
    {
        public Guid Id { get; set; }
        public Guid FileServerId { get; set; }
        public string Path { get; set; }

        public virtual tblFileServers tblFileServer { get; set; }
        public virtual ICollection<tblFiles> tblFiles { get; set; }
    }
}
