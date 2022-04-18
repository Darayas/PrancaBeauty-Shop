using System;
using System.Collections.Generic;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTab
{
    public class OutGetShowcaseTabForEdit
    {
        public string Id { get; set; }

        public string ShowcaseId { get; set; }

        public string Name { get; set; }

        public string BackgroundColorCode { get; set; }

        public bool IsEnable { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<OutGetShowcaseTabForEdit_Translate> LstTranslate { get; set; }
    }

    public class OutGetShowcaseTabForEdit_Translate
    {
        public string LangId { get; set; }

        public string Title { get; set; }
    }
}
