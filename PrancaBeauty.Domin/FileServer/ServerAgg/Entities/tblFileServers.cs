using Framework.Domain;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.FileServer.ServerAgg.Entities
{
    public class tblFileServers : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HttpDomin { get; set; }
        public string HttpPath { get; set; }
        public long Capacity { get; set; } // byte
        public string FtpData { get; set; }
        public bool IsActive { get; set; }


        public virtual ICollection<tblFiles> tblFiles { get; set; }
    }
}
