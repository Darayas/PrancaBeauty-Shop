using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Languages
{
    public class OutSiteLangCache
    {
        public string Id { get; set; }
        public string CountryId { get; set; }
        public string FlagUrl { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Abbr { get; set; }
        public string NativeName { get; set; }
        public bool IsRtl { get; set; }
    }
}
