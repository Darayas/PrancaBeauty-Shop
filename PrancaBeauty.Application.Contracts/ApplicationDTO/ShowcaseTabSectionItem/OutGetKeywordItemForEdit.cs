namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem
{
    public class OutGetKeywordItemForEdit
    {
        public string SectionItemId { get; set; }

        public string KeywordId { get; set; }

        public int CountFetch { get; set; }

        public OutGetKeywordItemForEditOrderByEnum OrderBy { get; set; }
    }

    public enum OutGetKeywordItemForEditOrderByEnum
    {
        Newest,
        Oldest,
        Popular
    }
}
