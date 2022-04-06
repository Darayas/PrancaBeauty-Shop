using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Settings
{
    public class OutSettings
    {
        public string LangCode { get; set; }
        public string SiteTitle { get; set; }
        public string SiteUrl { get; set; }
        public string SiteDescription { get; set; }
        public string SiteEmail { get; set; }
        public string SitePhoneNumber { get; set; }
        public bool IsInManufacture { get; set; }
    }
}
