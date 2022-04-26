using System.Collections.Generic;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem
{
    public class OutGetFreeItemForEdit
    {
        public string SectionItemId { get; set; }

        public string Name { get; set; }

        public List<OutGetFreeItemForEditTranslate> LstTranslate { get; set; }
    }

    public class OutGetFreeItemForEditTranslate
    {
        public string LangId { get; set; }

        public string FileId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string HtmlText { get; set; }
    }
}
