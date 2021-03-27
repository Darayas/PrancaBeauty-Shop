﻿using Framework.Domain;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.StettingsAgg.Entities;
using PrancaBeauty.Domin.TemplatesAgg.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Region.LanguagesAgg.Entities
{
    public class tblLanguages : IEntity
    {
        public Guid Id { get; set; }
        public Guid FlagImgId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Abbr { get; set; }
        public string NativeName { get; set; }
        public bool IsRtl { get; set; }
        public bool IsActive { get; set; }
        public bool UseForSiteLanguage { get; set; }

        public virtual ICollection<tblTamplates> tblTamplates { get; set; }
        public virtual ICollection<tblSettings> tblSettings { get; set; }
        public virtual tblFiles tblFile { get; set; }
    }
}
