using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase
{

    public class OutGetShowcaseForEdit
    {
        public string Id { get; set; }

        public string CountryId { get; set; }

        public string Name { get; set; }

        public string BackgroundColorCode { get; set; }

        public string CssStyle { get; set; }

        public string CssClass { get; set; }

        public bool IsFullWidth { get; set; }

        public bool IsEnable { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<OutGetShowcaseForEdit_Translate> LstTranslate { get; set; }
    }

    public class OutGetShowcaseForEdit_Translate
    {
        public string LangId { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
