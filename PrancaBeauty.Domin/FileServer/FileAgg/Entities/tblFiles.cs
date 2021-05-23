using Framework.Domain;
using PrancaBeauty.Domin.FileServer.ServerAgg.Entities;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.FileServer.FileAgg.Entities
{
    public class tblFiles : IEntity
    {
        public Guid Id { get; set; }
        public Guid FileServerId { get; set; }
        public Guid? UserId { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public long SizeOnDisk { get; set; }
        public string MimeType { get; set; } // image/jpg, image/png , application/zip
        public DateTime Date { get; set; }
        public bool IsPrivate { get; set; }


        public virtual tblFileServers tblFileServer { get; set; }
        public virtual tblUsers tblUser { get; set; }
        public virtual ICollection<tblLanguages> tblLanguages { get; set; }
        public virtual ICollection<tblCountries> tblCountries { get; set; }
    }
}
