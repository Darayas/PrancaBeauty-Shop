namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem
{
    public class OutGetCategoryItemForEdit
    {
        public string SectionItemId { get; set; }

        public string CategoryId { get; set; }

        public int CountFetch { get; set; }

        public OutGetCategoryItemForEditOrderByEnum OrderBy { get; set; }
    }

    public enum OutGetCategoryItemForEditOrderByEnum
    {
        Newest,
        Oldest,
        Popular
    }
}
